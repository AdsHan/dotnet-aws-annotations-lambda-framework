using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Lambda.Products.Data.Entities;
using Lambda.Products.Function;

namespace Lambda.Products.Data.Repositories;

public class ProductRepository : BaseRepository, IProductRepository
{
    private readonly IDynamoDBContext _context;

    public ProductRepository(IDynamoDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductModel>> GetAllAsync()
    {
        var result = await _context.ScanAsync<ProductModel>(default).GetRemainingAsync();

        return result.Count == 0 ? null : result;
    }

    public async Task<ProductModel> GetByIdAsync(Guid id)
    {
        var result = await _context.LoadAsync<ProductModel>(id);

        return result;
    }

    public async Task<BaseResult> AddAsync(ProductModel product)
    {
        var isIncluded = await SearchSku(product.Sku);

        if (isIncluded != null)
        {
            AddError("Este produto já existe!");
            return BaseResult;
        }

        var newProduct = new ProductModel();

        newProduct.Sku = product.Sku;
        newProduct.Name = product.Name;

        await _context.SaveAsync<ProductModel>(newProduct);

        BaseResult.Response = newProduct.Id;

        return BaseResult;
    }

    public async Task<BaseResult> UpdateAsync(ProductModel product)
    {
        var productUpdated = await _context.LoadAsync<ProductModel>(product.Id);

        if (productUpdated == null)
        {
            AddError("Não foi possível localizar o produto!");
            return BaseResult;
        }

        productUpdated.Sku = product.Sku;
        productUpdated.Name = product.Name;

        await _context.SaveAsync<ProductModel>(productUpdated);

        return BaseResult;
    }

    public async Task<BaseResult> DeleteAsync(Guid id)
    {
        var product = await GetByIdAsync(id);

        if (product == null)
        {
            AddError("Não foi possível localizar o produto!");
            return BaseResult;
        }

        await _context.DeleteAsync<ProductModel>(id);

        return BaseResult;
    }

    public async Task<IEnumerable<ProductModel>> SearchName(string text)
    {
        var scanConditions = new List<ScanCondition>();

        scanConditions.Add(new ScanCondition("Name", ScanOperator.Contains, text));

        var result = await _context.ScanAsync<ProductModel>(scanConditions, null).GetRemainingAsync();

        return result.Count == 0 ? null : result;
    }

    public async Task<IEnumerable<ProductModel>> SearchSku(string sku)
    {
        var scanConditions = new List<ScanCondition>();

        scanConditions.Add(new ScanCondition("Sku", ScanOperator.Equal, sku));

        var result = await _context.ScanAsync<ProductModel>(scanConditions, null).GetRemainingAsync();

        return result.Count == 0 ? null : result;
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}
