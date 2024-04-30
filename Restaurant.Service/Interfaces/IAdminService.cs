using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Admin;

namespace Restaurant.Service.Interfaces;

public interface IAdminService
{
    Task<Administrator?> Get(int id);
    Task<Administrator?> GetByLogin(string login);
    Task<IEnumerable<Administrator>?> Select();
    Task<bool> Create(AdminCreateRequest request);
    Task<bool> Update(AdminUpdateRequest request);
    Task<bool> Delete(AdminDeleteRequest request);
}