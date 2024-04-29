using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Repository;

public class PhoneAuthRepository : IPhoneAuthRepository
{
    private readonly AppDatabaseContext _db;

    public PhoneAuthRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<PhoneCode?> Get(string phone, string code)
    {
        return await _db.PhoneCodes.FirstOrDefaultAsync(i => i.Phone == phone && i.Code == code);
    }

    public async Task<PhoneCode> CreateCode(PhoneCode entity)
    {
        _db.PhoneCodes.Add(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteCode(PhoneCode entity)
    {
        _db.PhoneCodes.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}