using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Interfaces;

public interface IProductRepository
{
    Task<Product?> Get(int id);
    Task<Product?> GetByTitle(string title);
    Task<IEnumerable<Product?>> GetByLater(string word);
    Task<IEnumerable<Product?>> Select();
    Task<bool> Create(Product entity);
    Task<bool> Update(Product entity);
    Task<bool> Delete(Product entity);
}