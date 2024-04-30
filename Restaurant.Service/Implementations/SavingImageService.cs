using Microsoft.AspNetCore.Http;

namespace Restaurant.Service.Implementations;

public class SavingImageService
{
    private IHttpContextAccessor _httpContextAccessor;

    public SavingImageService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Save(IFormFile image, string path)
    {
        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var filePath = Path.Combine($"wwwroot/image/{path}", uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        string host = _httpContextAccessor.HttpContext.Request.Host.Value;
        return $"{host}/image/{path}/{uniqueFileName}";
    }
}