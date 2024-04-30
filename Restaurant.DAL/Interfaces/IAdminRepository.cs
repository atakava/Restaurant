using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Interfaces;

public interface IAdminRepository
{
    Task<Administrator?> Get(int id);
    Task<Administrator?> GetByLogin(string login);
    Task<IEnumerable<Administrator>?> Select();
    Task<bool> Create(Administrator entity);
    Task<bool> Update(Administrator entity);
    Task<bool> Delete(Administrator entity);
}