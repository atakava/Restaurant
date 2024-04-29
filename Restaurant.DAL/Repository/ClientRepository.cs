using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Repository;

public class ClientRepository : IClientRepository
{
    private readonly AppDatabaseContext _db;

    public ClientRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Client>> Select()
    {
        return await _db.Clients.ToListAsync();
    }

    public async Task<Client?> Get(int id)
    {
        return await _db.Clients.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Client?> GetByName(string name)
    {
        return await _db.Clients.FirstOrDefaultAsync(i => i.Name == name);
    }

    public async Task<Client?> GetByPhone(string phone)
    {
        return await _db.Clients.FirstOrDefaultAsync(i => i.Phone == phone);
    }

    public async Task<bool> Create(Client entity)
    {
        _db.Clients.Add(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update(Client entity)
    {
        var entry = _db.Attach(entity);
        entry.State = EntityState.Modified;

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(Client entity)
    {
        _db.Clients.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}