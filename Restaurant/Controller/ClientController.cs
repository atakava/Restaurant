using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Client;
using Restaurant.Domain.Response;

namespace Restaurant.Controller;

public class ClientController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] ClientGetRequest request)
    {
        var client = await ClientService.Get(request.Id);

        IBaseResponse<Client> response;

        if (client == null)
        {
            response = new BaseResponse<Client>(false, "Get Client: нет такого клиента по ID", null);

            return BadRequest(response);
        }

        response = new BaseResponse<Client>(true, null, client);

        return Ok(new
        {
            success = response.Success,
            data = new
            {
                response.Data!.Id,
                response.Data!.Name,
                response.Data!.Phone
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> GetByName([FromBody] ClientGetByNameRequest request)
    {
        var client = await ClientService.GetByName(request.Name);

        IBaseResponse<Client> response;

        if (client == null)
        {
            response = new BaseResponse<Client>(false, "Get Client: нет такого клиента по name", null);

            return BadRequest(response);
        }

        response = new BaseResponse<Client>(true, null, client);

        return Ok(new
        {
            success = response.Success,
            data = new
            {
                response.Data!.Id,
                response.Data!.Name,
                response.Data!.Phone
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> GetByPhone([FromBody] ClientGetByPhoneRequest request)
    {
        var client = await ClientService.GetByPhone(request.Phone);

        IBaseResponse<Client> response;

        if (client == null)
        {
            response = new BaseResponse<Client>(false, "Get Client: нет такого клиента по phone", null);

            return BadRequest(response);
        }

        response = new BaseResponse<Client>(true, null, client);


        return Ok(new
        {
            success = response.Success,
            data = new
            {
                response.Data!.Id,
                response.Data!.Name,
                response.Data!.Phone
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> Select()
    {
        var clients = await ClientService.Select();

        IBaseResponse<IEnumerable<Client>> response;

        if (clients.Count() == 0)
        {
            response = new BaseResponse<IEnumerable<Client>>(false, "Get Client: нет ни одного клиента", null);

            return BadRequest(response);
        }

        response = new BaseResponse<IEnumerable<Client>>(true, null, clients);


        return Ok(new
        {
            success = response.Success,
            data = response.Data!.Select(i => new
            {
                i.Id,
                i.Name,
                i.Phone
            })
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClientCreateRequest request)
    {
        var client = await ClientService.Create(request);

        IBaseResponse<bool> response;

        if (client == false)
        {
            response = new BaseResponse<bool>(false, "Ошибка создания пользователя", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        var user = await ClientService.GetByName(request.Name);

        if (response.Success && user != null)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var indentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(indentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] ClientUpdateRequest request)
    {
        var client = await ClientService.Update(request);

        IBaseResponse<bool> response;

        if (client == false)
        {
            response = new BaseResponse<bool>(false, "Ошибка обновления пользователя", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] ClientDeleteRequest request)
    {
        var client = await ClientService.Delete(request);

        IBaseResponse<bool> response;

        if (client == false)
        {
            response = new BaseResponse<bool>(false, "Ошибка удаления пользователя", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CurrentUser()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var user = await ClientService.Get(int.Parse(userId));

        var response = new BaseResponse<Client>(true, null, user);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("Вы успешно вышли из аккаунта");
    }
}