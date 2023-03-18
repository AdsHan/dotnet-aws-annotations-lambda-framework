using Amazon.DynamoDBv2.DataModel;
using Lambda.Products.Data.DomainObjects;

namespace Lambda.Products.Data.Entities;

[DynamoDBTable("products")]
public class ProductModel : BaseEntity, IAggregateRoot
{
    [DynamoDBProperty("sku")]
    public string Sku { get; set; }

    [DynamoDBProperty("name")]
    public string Name { get; set; }
}
