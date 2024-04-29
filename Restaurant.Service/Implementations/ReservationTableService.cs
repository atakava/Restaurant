using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.ReservationTable;
using Restaurant.Service.Interfaces;

namespace Restaurant.Service.Implementations;

public class ReservationTableService : IReservationTableService
{
    private readonly IReservationTableRepository _repository;

    public ReservationTableService(IReservationTableRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReservationTable?>> Select()
    {
        return await _repository.Select();
    }

    public async Task<ReservationTable?> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<bool> CheckTableReservation(int number, DateTime timeStart, DateTime timeEnd)
    {
        return await _repository.CheckTableReservation(number, timeStart, timeEnd);
    }

    public async Task<bool> Create(ReservationTableCreateRequest entity)
    {
        var reservation = await _repository.CheckTableReservation(entity.TableNumber, entity.Start, entity.End);

        if (reservation)
        {
            return false;
        }

        var reservationTable = new ReservationTable
        {
            TableId = entity.TableId,
            ClientId = entity.ClientId,
            End = entity.End,
            Start = entity.Start
        };

        await _repository.Create(reservationTable);

        return true;
    }

    public async Task<bool> Delete(ReservationTableDeleteRequest entity)
    {
        var reservation = await _repository.GetById(entity.Id);

        if (reservation == null)
        {
            return false;
        }

        await _repository.Delete(reservation);
        
        return true;
    }
}