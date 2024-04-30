using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Admin;
using Restaurant.Service.Interfaces;

namespace Restaurant.Service.Implementations;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _repository;

    public AdminService(IAdminRepository repository)
    {
        _repository = repository;
    }

    public async Task<Administrator?> Get(int id)
    {
        return await _repository.Get(id);
    }

    public async Task<Administrator?> GetByLogin(string login)
    {
        return await _repository.GetByLogin(login);
    }

    public async Task<IEnumerable<Administrator>?> Select()
    {
        return await _repository.Select();
    }

    public async Task<bool> Create(AdminCreateRequest request)
    {
        var salt = Guid.NewGuid().ToString();

        var admin = new Administrator
        {
            Login = request.Login,
            Password = PasswordHasher.HashPassword($"{request.Password}::{salt}"),
            Salt = salt,
            Role = "Admin"
        };

        await _repository.Create(admin);

        return true;
    }

    public async Task<bool> Update(AdminUpdateRequest request)
    {
        var admin = await _repository.Get(request.Id);

        if (admin == null)
            return false;

        admin.Login = request.Login;

        await _repository.Update(admin);

        return true;
    }

    public async Task<bool> Delete(AdminDeleteRequest request)
    {
        var admin = await _repository.Get(request.Id);

        if (admin == null)
            return false;

        await _repository.Delete(admin);

        return true;
    }
}