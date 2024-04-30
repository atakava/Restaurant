using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Product;

namespace Restaurant.Service.Interfaces;

public interface IProductService
{
    Task<Product?> Get(int id);
    Task<Product?> GetByTitle(string title);
    Task<IEnumerable<Product?>> GetByLater(string word);
    Task<IEnumerable<Product?>> Select();
    Task<bool> Create(ProductCreateRequest request);
    Task<bool> Update(ProductUpdateRequest request);
    Task<bool> Delete(ProductDeleteRequest request);
}