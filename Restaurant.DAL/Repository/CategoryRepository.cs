using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDatabaseContext _db;

    public CategoryRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<Category?> Get(int id)
    {
        return await _db.Categories.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Category?> GetByName(string name)
    {
        return await _db.Categories.FirstOrDefaultAsync(i => i.Name == name);
    }

    public async Task<IEnumerable<Category?>> Select()
    {
        return await _db.Categories.ToListAsync();
    }

    public async Task<bool> Create(Category entity)
    {
        _db.Categories.Add(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update(Category entity)
    {
        var entry = _db.Attach(entity);
        entry.State = EntityState.Modified;

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(Category entity)
    {
        _db.Categories.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}