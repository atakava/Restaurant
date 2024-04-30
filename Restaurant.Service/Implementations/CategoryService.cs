using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Category;
using Restaurant.Service.Interfaces;

namespace Restaurant.Service.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Category?> Get(int id)
    {
        return await _repository.Get(id);
    }

    public async Task<Category?> GetByName(string name)
    {
        return await _repository.GetByName(name);
    }

    public async Task<IEnumerable<Category?>> Select()
    {
        return await _repository.Select();
    }

    public async Task<bool> Create(CategoryCreateRequest request)
    {
        var category = await _repository.GetByName(request.Name);

        if (category == null)
        {
            category = new Category
            {
                Name = request.Name
            };
            
            await _repository.Create(category);

            return true;
        }
        
        return false;
    }

    public async Task<bool> Update(CategoryUpdateRequest request)
    {
        var category = await _repository.Get(request.Id);

        if (category != null)
        {
            category.Name = request.Name;
            
            await _repository.Update(category);

            return true;
        }
        
        return false;
    }

    public async Task<bool> Delete(CategoryDeleteRequest request)
    {
        var category = await _repository.Get(request.Id);

        if (category != null)
        {
            await _repository.Delete(category);

            return true;
        }
        
        return false;
    }
}