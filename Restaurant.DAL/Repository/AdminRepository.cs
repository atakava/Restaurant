using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Admin;

namespace Restaurant.DAL.Repository;

public class AdminRepository : IAdminRepository
{
    private readonly AppDatabaseContext _db;

    public AdminRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<Administrator?> Get(int id)
    {
        return await _db.Administrators.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Administrator?> GetByLogin(string login)
    {
        return await _db.Administrators.FirstOrDefaultAsync(i => i.Login == login);
    }

    public async Task<IEnumerable<Administrator>?> Select()
    {
        return await _db.Administrators.ToListAsync();
    }

    public async Task<bool> Create(Administrator entity)
    {
        _db.Administrators.Add(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Login(string login, string password)
    {
        var verify =
            await _db.Administrators
                .FirstOrDefaultAsync(i => i.Login == login && i.Password == password);

        if (verify == null)
            return false;

        return true;
    }

    public async Task<bool> Update(Administrator entity)
    {
        var entry = _db.Attach(entity);
        entry.State = EntityState.Modified;

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(Administrator entity)
    {
        _db.Administrators.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}