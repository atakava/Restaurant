using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Request.PhoneCode;
using Restaurant.Domain.Response;

namespace Restaurant.Controller;

public class PhoneAuthController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Generate([FromBody] PhoneCodeGenerateRequest request)
    {
        var user = await ClientService.GetByPhone(request.Phone);

        IBaseResponse<string> response;

        if (user == null)
        {
            response = new BaseResponse<string>(false, "Ошибка пользователя с таким телефоном не существует", null);

            return BadRequest(response);
        }

        await PhoneAuthService.GenerateCode(request);

        response = new BaseResponse<string>(true, null, "код выслан по смс");

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Verify([FromBody] PhoneCodeVerifyRequest request)
    {
        var user = await ClientService.GetByPhone(request.Phone);

        IBaseResponse<string> response;

        if (user == null)
        {
            response = new BaseResponse<string>(false, "Ошибка пользователя с таким телефоном не существует", null);

            return BadRequest(response);
        }

        var code = await PhoneAuthService.VerifyCode(request);

        if (code)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var indentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(indentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            response = new BaseResponse<string>(true, null, "Вы успешно зашли на аккаунт");

            return Ok(response);
        }

        response = new BaseResponse<string>(false, "Верификация не пройдена проверьте номер или код", null);
        
        return BadRequest(response);
    }
}