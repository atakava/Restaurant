using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> Get(int id);
    Task<Category?> GetByName(string name);
    Task<IEnumerable<Category?>> Select();
    Task<bool> Create(Category entity);
    Task<bool> Update(Category entity);
    Task<bool> Delete(Category entity);
}