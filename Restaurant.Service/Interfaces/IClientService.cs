using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Client;

namespace Restaurant.Service.Interfaces;

public interface IClientService
{
    Task<IEnumerable<Client>> Select();
    Task<Client?> Get(int id);
    Task<Client?> GetByName(string name);
    Task<Client?> GetByPhone(string phone);
    Task<bool> Create(ClientCreateRequest request);
    Task<bool> Update(ClientUpdateRequest request);
    Task<bool> Delete(ClientDeleteRequest request);
}