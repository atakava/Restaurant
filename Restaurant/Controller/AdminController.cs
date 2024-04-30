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
            response = new BaseResponse<IEnumerable<Administrator>>(false, "Get Admin Select: нет ни одного admin",
                null);

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
        var user = await AdminService.Create(request);

        IBaseResponse<bool> response;

        if (user == false)
        {
            response = new BaseResponse<bool>(false, "Ошибка создания admin", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        var admin = await AdminService.GetByLogin(request.Login);

        if (response.Success && admin != null)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                new Claim(ClaimTypes.Name, admin.Login),
            };

            var indentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(indentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] AdminLoginRequest request)
    {
        var admin = await AdminService.Login(request);

        IBaseResponse<bool> response;

        if (!admin)
        {
            response = new BaseResponse<bool>(false, "Ошибка входа в систему, проверьте данные", false);

            return BadRequest(response);
        }

        var currentAdmin = await AdminService.GetByLogin(request.Login);
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, currentAdmin.Id.ToString()),
            new Claim(ClaimTypes.Name, currentAdmin.Login),
        };

        var indentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(indentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        
        response = new BaseResponse<bool>(true, null, true);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] AdminUpdateRequest request)
    {
        var admin = await AdminService.Update(request);

        IBaseResponse<bool> response;

        if (admin == false)
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
        var admin = await AdminService.Delete(request);

        IBaseResponse<bool> response;

        if (admin == false)
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