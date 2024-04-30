using Microsoft.AspNetCore.Http;

namespace Restaurant.Domain.Request.Product;

public class ProductUpdateRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IFormFile? Img { get; set; }
    public int? Price { get; set; }

    public int? CatalogId { get; set; }
}