using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Table;

namespace Restaurant.Service.Interfaces;

public interface ITableService
{
    Task<IEnumerable<Table>> Select();
    Task<Table?> Get(int id);
    Task<Table?> GetByNumber(int number);
    Task<Table?> GetByNumberOfSeats(int numberOfSeats);
    Task<bool> Create(TableCreateRequest entity);
    Task<bool> Update(TableUpdateRequest entity);
    Task<bool> Delete(TableDeleteRequest entity);
}