using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Repository;

public class ReservationTableRepository : IReservationTableRepository
{
    private readonly AppDatabaseContext _db;

    public ReservationTableRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ReservationTable?>> Select()
    {
        return await _db.ReservationTables.ToListAsync();
    }

    public async Task<ReservationTable?> Get(int number, DateTime timeStart, DateTime timeEnd)
    {
        return await _db.ReservationTables
            .Include(i => i.Table)
            .Include(i => i.Client)
            .FirstOrDefaultAsync(i => i.Table.Number == number && i.Start == timeStart && i.End == timeEnd);
    }

    public async Task<bool> CheckTableReservation(int number, DateTime timeStart, DateTime timeEnd)
    {
        return await _db.ReservationTables
            .Include(i => i.Table)
            .Include(i => i.Client)
            .AnyAsync(r => r.Table.Number == number &&
                           ((timeStart >= r.Start && timeStart < r.End) || (timeEnd > r.Start && timeEnd <= r.End) ||
                            (timeStart <= r.Start && timeEnd >= r.End)));
    }

    public async Task<ReservationTable?> GetById(int id)
    {
        return await _db.ReservationTables
            .Include(i => i.Table)
            .Include(i => i.Client)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<bool> Create(ReservationTable entity)
    {
        _db.ReservationTables.Add(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(ReservationTable entity)
    {
        _db.ReservationTables.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}