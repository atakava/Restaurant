using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Interfaces;

public interface IReservationTableRepository
{
    Task<IEnumerable<ReservationTable?>> Select();
    Task<ReservationTable?> Get(int number, DateTime timeStart, DateTime timeEnd);
    Task<ReservationTable?> GetById(int id);
    Task<bool> CheckTableReservation(int number, DateTime timeStart, DateTime timeEnd);
    Task<bool> Create(ReservationTable entity);
    Task<bool> Delete(ReservationTable entity);
}