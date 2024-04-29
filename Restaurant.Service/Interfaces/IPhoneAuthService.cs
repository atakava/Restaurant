using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.PhoneCode;

namespace Restaurant.Service.Interfaces;

public interface IPhoneAuthService
{
    Task<bool> GenerateCode(PhoneCodeGenerateRequest request);
    Task<bool> VerifyCode(PhoneCodeVerifyRequest request);
}