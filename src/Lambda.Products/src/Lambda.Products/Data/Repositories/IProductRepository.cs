using Lambda.Products.Data.DomainObjects;
using Lambda.Products.Data.Entities;
using Lambda.Products.Function;

namespace Lambda.Products.Data.Repositories;

public interface IProductRepository : IRepository<ProductModel>
{
    Task<IEnumerable<ProductModel>> GetAllAsync();
    Task<ProductModel> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductModel>> SearchName(string text);
    Task<BaseResult> AddAsync(ProductModel product);
    Task<BaseResult> UpdateAsync(ProductModel product);
    Task<BaseResult> DeleteAsync(Guid id);
}
