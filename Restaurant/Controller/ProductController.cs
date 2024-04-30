using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Product;
using Restaurant.Domain.Response;

namespace Restaurant.Controller;

public class ProductController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] ProductGetRequest request)
    {
        var product = await ProductService.Get(request.Id);

        IBaseResponse<Product> response;

        if (product == null)
        {
            response = new BaseResponse<Product>(false, "Get Product: нет такого product по ID", null);

            return BadRequest(response);
        }

        response = new BaseResponse<Product>(true, null, product);

        return Ok(new
        {
            success = response.Success,
            data = new
            {
                response.Data!.Id,
                response.Data!.Title,
                response.Data!.Description,
                response.Data!.Price,
                response.Data!.CatalogId,
                response.Data!.Category.Name,
                response.Data!.Img,
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> GetByTitle([FromBody] ProductGetByTitleRequest request)
    {
        var product = await ProductService.GetByTitle(request.Title);

        IBaseResponse<Product> response;

        if (product == null)
        {
            response = new BaseResponse<Product>(false, "Get Product: нет такого product по Title", null);

            return BadRequest(response);
        }

        response = new BaseResponse<Product>(true, null, product);

        return Ok(new
        {
            success = response.Success,
            data = new
            {
                response.Data!.Id,
                response.Data!.Title,
                response.Data!.Description,
                response.Data!.Price,
                response.Data!.CatalogId,
                response.Data!.Category.Name,
                response.Data!.Img,
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> GetByLater([FromBody] ProductGetByLaterRequest request)
    {
        var products = await ProductService.GetByLater(request.Word);

        IBaseResponse<IEnumerable<Product>> response;

        if (products.Count() == 0)
        {
            response = new BaseResponse<IEnumerable<Product>>(false, "Get Product: нет такого product по Later", null);

            return BadRequest(response);
        }

        response = new BaseResponse<IEnumerable<Product>>(true, null, products);

        return Ok(new
        {
            success = response.Success,
            data = response.Data!.Select(i => new
            {
                i.Id,
                i.Title,
                i.Description,
                i.Price,
                i.CatalogId,
                i.Category.Name,
                i.Img,
            })
        });
    }

    [HttpPost]
    public async Task<IActionResult> Select()
    {
        var products = await ProductService.Select();

        IBaseResponse<IEnumerable<Product>> response;

        if (products.Count() == 0)
        {
            response = new BaseResponse<IEnumerable<Product>>(false, "Get Product: нет такого product по Later", null);

            return BadRequest(response);
        }

        response = new BaseResponse<IEnumerable<Product>>(true, null, products);

        return Ok(new
        {
            success = response.Success,
            data = response.Data!.Select(i => new
            {
                i.Id,
                i.Title,
                i.Description,
                i.Price,
                i.CatalogId,
                i.Category.Name,
                i.Img,
            })
        });
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
    {
        var product = await ProductService.Create(request);

        IBaseResponse<bool> response;

        if (product == false)
        {
            response = new BaseResponse<bool>(false, "Get Product: нет такого product по ID", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] ProductDeleteRequest request)
    {
        var product = await ProductService.Delete(request);

        IBaseResponse<bool> response;

        if (product == false)
        {
            response = new BaseResponse<bool>(false, "Get Product: нет такого product по ID", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        return Ok(response);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
    {
        var product = await ProductService.Update(request);

        IBaseResponse<bool> response;

        if (product == false)
        {
            response = new BaseResponse<bool>(false, "Get Product: нет такого product по ID", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        return Ok(response);
    }
}