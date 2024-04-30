using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Client;
using Restaurant.Service.Interfaces;

namespace Restaurant.Service.Implementations;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Client>> Select()
    {
        return await _repository.Select();
    }

    public async Task<Client?> Get(int id)
    {
        return await _repository.Get(id);
    }

    public async Task<Client?> GetByName(string name)
    {
        return await _repository.GetByName(name);
    }

    public async Task<Client?> GetByPhone(string phone)
    {
        return await _repository.GetByPhone(phone);
    }

    public async Task<bool> Create(ClientCreateRequest request)
    {
        var client = await _repository.GetByPhone(request.Phone);

        if (client != null)
            return false;

        client = new Client
        {
            Name = request.Name,
            Phone = request.Phone,
            Role = "client"
        };

        await _repository.Create(client);

        return true;
    }

    public async Task<bool> Update(ClientUpdateRequest request)
    {
        var client = await _repository.Get(request.Id);

        if (client == null)
            return false;

        client.Name = request.Name ?? client.Name;
        client.Phone = request.Phone ?? client.Phone;

        await _repository.Update(client);

        return true;
    }

    public async Task<bool> Delete(ClientDeleteRequest request)
    {
        var client = await _repository.Get(request.Id);

        if (client == null)
            return false;

        await _repository.Delete(client);

        return true;
    }
}