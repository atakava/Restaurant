using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Table;
using Restaurant.Domain.Response;

namespace Restaurant.Controller;

public class TableController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Select()
    {
        var tables = await TableService.Select();

        return Ok(tables);
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] TableGetRequest request)
    {
        var table = await TableService.Get(request.Id);

        IBaseResponse<Table> response;

        if (table == null)
        {
            response = new BaseResponse<Table>(false, "Нет стола с таким ID", null);

            return BadRequest(response);
        }

        response = new BaseResponse<Table>(true, null, table);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> GetByNumber([FromBody] TableGetByNumberRequest request)
    {
        var table = await TableService.GetByNumber(request.Number);

        IBaseResponse<Table> response;
    
        if (table == null)
        {
            response = new BaseResponse<Table>(false, "Нет стола с таким номером", null);

            return BadRequest(response);
        }

        response = new BaseResponse<Table>(true, null, table);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> GetByNumberOfSeats([FromBody] TableGetByNumberOfSeatsRequest request)
    {
        var table = await TableService.GetByNumberOfSeats(request.NumberOfSeats);

        IBaseResponse<Table> response;
    
        if (table == null)
        {
            response = new BaseResponse<Table>(false, "Нет стола с таким количеством мест", null);

            return BadRequest(response);
        }

        response = new BaseResponse<Table>(true, null, table);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TableCreateRequest request)
    {
        var table = await TableService.Create(request);

        IBaseResponse<bool> response;
    
        if (!table)
        {
            response = new BaseResponse<bool>(false, "Ошибка создания стола", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, table);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] TableUpdateRequest request)
    {
        var table = await TableService.Update(request);
        
        IBaseResponse<bool> response;
    
        if (!table)
        {
            response = new BaseResponse<bool>(false, "Ошибка обновления стола", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, table);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] TableDeleteRequest request)
    {
        var table = await TableService.Delete(request);

        IBaseResponse<bool> response;
    
        if (!table)
        {
            response = new BaseResponse<bool>(false, "Ошибка удаления стола", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, table);
        
        return Ok(response);
    }
}