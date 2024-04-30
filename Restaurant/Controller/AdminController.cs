using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Admin;
using Restaurant.Domain.Response;

namespace Restaurant.Controller;

public class AdminController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] AdminGetRequest request)
    {
        var admin = await AdminService.Get(request.Id);

        IBaseResponse<Administrator> response;

        if (admin == null)
        {
            response = new BaseResponse<Administrator>(false, "Get Admin: нет такого клиента по ID", null);

            return BadRequest(response);
        }

        response = new BaseResponse<Administrator>(true, null, admin);

        return Ok(new
        {
            success = response.Success,
            data = new
            {
                response.Data!.Id,
                response.Data!.Login,
                response.Data!.Role
            }
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> GetByLogin([FromBody] AdminGetByLoginRequest request)
    {
        var admin = await AdminService.GetByLogin(request.Login);

        IBaseResponse<Administrator> response;

        if (admin == null)
        {
            response = new BaseResponse<Administrator>(false, "Get Admin: нет такого клиента по логину", null);

            return BadRequest(response);
        }

        response = new BaseResponse<Administrator>(true, null, admin);

        return Ok(new
        {
            success = response.Success,
            data = new
            {
                response.Data!.Id,
                response.Data!.Login,
                response.Data!.Role
            }
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Select()
    {
        var admins = await AdminService.Select();

        IBaseResponse<IEnumerable<Administrator>> response;

        if (admins.Count() == 0)
        {
            response = new BaseResponse<IEnumerable<Administrator>>(false, "Get Admin Select: нет ни одного admin", null);

            return BadRequest(response);
        }

        response = new BaseResponse<IEnumerable<Administrator>>(true, null, admins);


        return Ok(new
        {
            success = response.Success,
            data = response.Data!.Select(i => new
            {
                i.Id,
                i.Login,
                i.Role
            })
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AdminCreateRequest request)
    {
        var client = await AdminService.Create(request);

        IBaseResponse<bool> response;

        if (client == false)
        {
            response = new BaseResponse<bool>(false, "Ошибка создания admin", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        var user = await AdminService.GetByLogin(request.Login);

        if (response.Success && user != null)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
            };

            var indentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(indentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] AdminUpdateRequest request)
    {
        var client = await AdminService.Update(request);

        IBaseResponse<bool> response;

        if (client == false)
        {
            response = new BaseResponse<bool>(false, "Ошибка обновления admin", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] AdminDeleteRequest request)
    {
        var client = await AdminService.Delete(request);

        IBaseResponse<bool> response;

        if (client == false)
        {
            response = new BaseResponse<bool>(false, "Ошибка удаления admin", false);

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

        var user = await AdminService.Get(int.Parse(userId));

        var response = new BaseResponse<Administrator>(true, null, user);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("Вы успешно вышли из аккаунта");
    }
}