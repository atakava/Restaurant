using Restaurant.Domain.Entity;

namespace Restaurant.DAL.Interfaces;

public interface IPhoneAuthRepository
{
    
    Task<PhoneCode?> Get(string phone, string code);
    Task<PhoneCode> CreateCode(PhoneCode entity);
    Task<bool> DeleteCode(PhoneCode entity);
}