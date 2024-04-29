namespace Restaurant.Domain.Request.PhoneCode;

public class PhoneCodeVerifyRequest
{
    public string Phone { get; set; }
    public string Code { get; set; }
}