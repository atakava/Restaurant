using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Category;
using Restaurant.Domain.Response;

namespace Restaurant.Controller;

public class CategoryController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] CategoryGetRequest request)
    {
        var category = await CategoryService.Get(request.Id);

        IBaseResponse<Category> response;

        if (category == null)
        {
            response = new BaseResponse<Category>(false, "Get category: нет такого category по ID", null);

            return BadRequest(response);
        }

        response = new BaseResponse<Category>(true, null, category);

        return Ok(new
        {
            success = response.Success,
            data = new
            {
                response.Data!.Id,
                response.Data!.Name,
                products = response.Data.Products!.Select(p => new
                {
                    p.Title,
                    p.Description,
                    p.Price,
                    p.Img,
                })
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> GetByName([FromBody] CategoryGetByNameRequest request)
    {
        var category = await CategoryService.GetByName(request.Name);

        IBaseResponse<Category> response;

        if (category == null)
        {
            response = new BaseResponse<Category>(false, "Get category: нет такого category по name", null);

            return BadRequest(response);
        }

        response = new BaseResponse<Category>(true, null, category);

        return Ok(new
        {
            success = response.Success,
            data = new
            {
                response.Data!.Id,
                response.Data!.Name,
                products = response.Data.Products!.Select(p => new
                {
                    p.Title,
                    p.Description,
                    p.Price,
                    p.Img,
                })
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> Select()
    {
        var categorys = await CategoryService.Select();

        IBaseResponse<IEnumerable<Category>> response;

        if (categorys.Count() == 0)
        {
            response = new BaseResponse<IEnumerable<Category>>(false, "select category: нет такого category по name",
                null);

            return BadRequest(response);
        }

        response = new BaseResponse<IEnumerable<Category>>(true, null, categorys);

        return Ok(new
        {
            success = response.Success,
            data = response.Data!.Select(i => new
            {
                i.Id,
                i.Name,
                products = i.Products!.Select(p => new
                {
                    p.Title,
                    p.Description,
                    p.Price,
                    p.Img
                })
            })
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateRequest request)
    {
        var category = await CategoryService.Create(request);

        IBaseResponse<bool> response;

        if (category == false)
        {
            response = new BaseResponse<bool>(false, "Create category: нет такого category по ID", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] CategoryUpdateRequest request)
    {
        var category = await CategoryService.Update(request);

        IBaseResponse<bool> response;

        if (category == false)
        {
            response = new BaseResponse<bool>(false, "Update category: нет такого category по ID", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] CategoryDeleteRequest request)
    {
        var category = await CategoryService.Delete(request);

        IBaseResponse<bool> response;

        if (category == false)
        {
            response = new BaseResponse<bool>(false, "Update category: нет такого category по ID", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        return Ok(response);
    }
}