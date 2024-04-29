using Microsoft.AspNetCore.Mvc;
using Restaurant.Service.Interfaces;

namespace Restaurant.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class BaseController : ControllerBase
{
    protected IClientService ClientService =>
        HttpContext.RequestServices.GetRequiredService<IClientService>();
}