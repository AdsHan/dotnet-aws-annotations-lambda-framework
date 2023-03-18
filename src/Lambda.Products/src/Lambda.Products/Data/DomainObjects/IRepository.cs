namespace Lambda.Products.Data.DomainObjects;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
}
