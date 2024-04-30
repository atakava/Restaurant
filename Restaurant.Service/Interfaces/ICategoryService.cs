using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Category;

namespace Restaurant.Service.Interfaces;

public interface ICategoryService
{
    Task<Category?> Get(int id);
    Task<Category?> GetByName(string name);
    Task<IEnumerable<Category?>> Select();
    Task<bool> Create(CategoryCreateRequest request);
    Task<bool> Update(CategoryUpdateRequest request);
    Task<bool> Delete(CategoryDeleteRequest request);
}