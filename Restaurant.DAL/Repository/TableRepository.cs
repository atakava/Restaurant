using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Repository;

public class TableRepository : ITableRepository
{
    private readonly AppDatabaseContext _db;

    public TableRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Table>> Select()
    {
        return await _db.Tables.ToListAsync();
    }

    public async Task<Table?> Get(int id)
    {
        return await _db.Tables.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Table?> GetByNumber(int number)
    {
        return await _db.Tables.FirstOrDefaultAsync(i => i.Number == number);
    }

    public async Task<Table?> GetByNumberOfSeats(int numberOfSeats)
    {
        return await _db.Tables.FirstOrDefaultAsync(i => i.NumberOfSeats == numberOfSeats);
    }

    public async Task<bool> Create(Table entity)
    {
        _db.Tables.Add(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update(Table entity)
    {
        var entry = _db.Attach(entity);
        entry.State = EntityState.Modified;

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(Table entity)
    {
        _db.Tables.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}