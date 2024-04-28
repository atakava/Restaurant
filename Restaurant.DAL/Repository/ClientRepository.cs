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
        var client = await _db.Clients.FirstOrDefaultAsync(i => i.Id == id);

        if (client == null || client.Id == 0)
        {
            throw new Exception("Клиент не найден по ID");
        }

        return client;
    }

    public async Task<Client?> GetByName(string name)
    {
        var client = await _db.Clients.FirstOrDefaultAsync(i => i.Name == name);

        if (client == null)
        {
            throw new Exception("Клиент не найден по имени");
        }

        return client;
    }

    public async Task<Client?> GetByPhone(string phone)
    {
        var client = await _db.Clients.FirstOrDefaultAsync(i => i.Phone == phone);

        if (client == null)
        {
            throw new Exception("Клиент не найден по номеру телефона");
        }

        return client;
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