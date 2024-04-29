using Microsoft.AspNetCore.Mvc;
using Restaurant.Service.Interfaces;

namespace Restaurant.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class BaseController : ControllerBase
{
    protected IClientService ClientService =>
        HttpContext.RequestServices.GetRequiredService<IClientService>();
    
    protected IPhoneAuthService PhoneAuthService =>
        HttpContext.RequestServices.GetRequiredService<IPhoneAuthService>();
    
    protected ITableService TableService =>
        HttpContext.RequestServices.GetRequiredService<ITableService>();
}