using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Core;
using Lambda.Products.Data.Entities;
using Lambda.Products.Data.Repositories;
using Lambda.Products.Function;
using Product.API.Application.InputModels;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda.Products
{
    public class Functions : BaseFunction
    {

        private readonly IProductRepository _productRepository;

        public Functions(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [LambdaFunction()]
        [HttpApi(LambdaHttpMethod.Get, "/products")]
        public async Task<IHttpResult> GetAllAsync(ILambdaContext context)
        {
            var result = await _productRepository.GetAllAsync();

            return CustomResponse(result);
        }

        [LambdaFunction()]
        [HttpApi(LambdaHttpMethod.Get, "/products/{id}")]
        public async Task<IHttpResult> GetByIdAsync(string id, ILambdaContext context)
        {
            var result = await _productRepository.GetByIdAsync(Guid.Parse(id));

            return CustomResponse(result);
        }

        [LambdaFunction()]
        [HttpApi(LambdaHttpMethod.Get, "/products/search/{text}")]
        public async Task<IHttpResult> GetSearchAsync(string text, ILambdaContext context)
        {
            var result = await _productRepository.SearchName(text);

            return CustomResponse(result);
        }

        [LambdaFunction()]
        [HttpApi(LambdaHttpMethod.Post, "/products")]
        public async Task<IHttpResult> CreateProduct([FromBody] CreateProductInputModel input, ILambdaContext context)
        {

            var product = new ProductModel()
            {
                Sku = input.Sku,
                Name = input.Name,
            };

            var result = await _productRepository.AddAsync(product);

            return CustomResponse(result);
        }

        [LambdaFunction()]
        [HttpApi(LambdaHttpMethod.Put, "/products")]
        public async Task<IHttpResult> UpdateProduct([FromBody] UpdateProductInputModel input, ILambdaContext context)
        {
            var product = new ProductModel()
            {
                Id = Guid.Parse(input.Id),
                Sku = input.Sku,
                Name = input.Name
            };

            var result = await _productRepository.UpdateAsync(product);

            return CustomResponse(result);
        }

        [LambdaFunction()]
        [HttpApi(LambdaHttpMethod.Delete, "/products/{id}")]
        public async Task<IHttpResult> DeleteProduct(string id, ILambdaContext context)
        {
            var result = await _productRepository.DeleteAsync(Guid.Parse(id));

            return CustomResponse(result);
        }

    }
}