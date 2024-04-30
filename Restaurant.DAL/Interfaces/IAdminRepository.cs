using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Admin;

namespace Restaurant.DAL.Interfaces;

public interface IAdminRepository
{
    Task<Administrator?> Get(int id);
    Task<Administrator?> GetByLogin(string login);
    Task<IEnumerable<Administrator>?> Select();
    Task<bool> Create(Administrator entity);
    Task<bool> Login(string login, string password);
    Task<bool> Update(Administrator entity);
    Task<bool> Delete(Administrator entity);
}