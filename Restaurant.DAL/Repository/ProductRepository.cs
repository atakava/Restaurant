using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDatabaseContext _db;

    public ProductRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<Product?> Get(int id)
    {
        return await _db.Products.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Product?> GetByTitle(string title)
    {
        return await _db.Products.FirstOrDefaultAsync(i => i.Title == title);
    }

    public async Task<IEnumerable<Product?>> GetByLater(string word)
    {
        return await _db.Products
            .Where(
                p => p.Title.Contains(word) ||
                     p.Description.Contains(word) ||
                     p.Category.Name.Contains(word)
            )
            .ToListAsync();
    }

    public async Task<IEnumerable<Product?>> Select()
    {
        return await _db.Products.ToListAsync();
    }

    public async Task<bool> Create(Product entity)
    {
        _db.Products.Add(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update(Product entity)
    {
        var entry = _db.Attach(entity);
        entry.State = EntityState.Modified;

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(Product entity)
    {
        _db.Products.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}