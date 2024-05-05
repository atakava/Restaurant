using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.PhoneCode;

namespace Restaurant.Service.Interfaces;

public interface IPhoneAuthService
{
    Task<PhoneCode> GenerateCode(PhoneCodeGenerateRequest request);
    Task<bool> VerifyCode(PhoneCodeVerifyRequest request);
}