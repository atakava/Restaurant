using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Interfaces;

public interface ITableRepository
{
    Task<IEnumerable<Table>> Select();
    Task<Table?> Get(int id);
    Task<Table?> GetByNumber(int number);
    Task<Table?> GetByNumberOfSeats(int numberOfSeats);
    Task<bool> Create(Table entity);
    Task<bool> Update(Table entity);
    Task<bool> Delete(Table entity);
}