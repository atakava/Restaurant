using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.ReservationTable;

namespace Restaurant.Service.Interfaces;

public interface IReservationTableService
{
    Task<IEnumerable<ReservationTable?>> Select();
    Task<ReservationTable?> GetById(int id);
    Task<bool> CheckTableReservation(int number, DateTime timeStart, DateTime timeEnd);
    Task<bool> Create(ReservationTableCreateRequest entity);
    Task<bool> Delete(ReservationTableDeleteRequest entity);
}