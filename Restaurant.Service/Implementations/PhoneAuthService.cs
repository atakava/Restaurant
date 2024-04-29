using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.PhoneCode;
using Restaurant.Service.Interfaces;

namespace Restaurant.Service.Implementations;

public class PhoneAuthService : IPhoneAuthService
{
    private readonly IPhoneAuthRepository _phoneRepository;
    private readonly IClientRepository _clientRepository;

    public PhoneAuthService(IPhoneAuthRepository phoneRepository, IClientRepository clientRepository)
    {
        _phoneRepository = phoneRepository;
        _clientRepository = clientRepository;
    }

    public async Task<bool> GenerateCode(PhoneCodeGenerateRequest request)
    {
        var client = await _clientRepository.GetByPhone(request.Phone);

        if (client == null)
        {
            return false;
        }
        
        var code = SmsService.GenerateVerificationCode();

        var generateCode = new PhoneCode
        {
            Phone = request.Phone,
            Code = code
        };

        await _phoneRepository.CreateCode(generateCode);

        return true;
    }

    public async Task<bool> VerifyCode(PhoneCodeVerifyRequest request)
    {
        var verifyCode = await _phoneRepository.Get(request.Phone, request.Code);

        if (verifyCode == null)
            return false;

        await _phoneRepository.DeleteCode(verifyCode);

        return true;
    }
}