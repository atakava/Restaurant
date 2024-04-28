using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Interfaces;

public interface IClientRepository
{
    Task<IEnumerable<Client>> Select();
    Task<Client?> Get(int id);
    Task<Client?> GetByName(string name);
    Task<Client?> GetByPhone(string phone);
    Task<bool> Create(Client entity);
    Task<bool> Update(Client entity);
    Task<bool> Delete(Client entity);
}