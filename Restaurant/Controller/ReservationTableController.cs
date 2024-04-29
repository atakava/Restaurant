using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Client;
using Restaurant.Domain.Request.ReservationTable;
using Restaurant.Domain.Response;

namespace Restaurant.Controller;

public class ReservationTableController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Select([FromBody] ClientGetRequest request)
    {
        var reservationTables = await ReservationTableService.Select();

        IBaseResponse<IEnumerable<ReservationTable>> response;

        if (reservationTables.Count() == 0)
        {
            response = new BaseResponse<IEnumerable<ReservationTable>>(true, "у вас 0 зарезервированых столиков", null);

            return BadRequest(response);
        }

        response = new BaseResponse<IEnumerable<ReservationTable>>(true, null, reservationTables);

        return Ok(new
        {
            success = response.Success,
            data = response.Data!.Select(i => new
            {
                id = i.Id,
                start = i.Start,
                end = i.End,
                clientId = i.ClientId,
                tableId = i.TableId,
                user = new
                {
                    i.Client.Name,
                    i.Client.Phone
                },
                table = new
                {
                    i.Table.Number,
                    i.Table.NumberOfSeats,
                    i.Table.Type,
                    i.Table.Price,
                }
            })
        });
    }

    [HttpPost]
    public async Task<IActionResult> GetById([FromBody] ReservationTableGetByIdRequest request)
    {
        var table = await ReservationTableService.GetById(request.Id);

        IBaseResponse<ReservationTable> response;

        if (table == null)
        {
            response = new BaseResponse<ReservationTable>(false, "Нет зарезервированого стола с таким ID", null);

            return BadRequest(response);
        }

        response = new BaseResponse<ReservationTable>(true, null, table);

        return Ok(new
        {
            success = response.Success,
            data = new
            {
                id = response.Data!.Id,
                start = response.Data.Start,
                end = response.Data.End,
                clientId = response.Data.ClientId,
                tableId = response.Data.TableId,
                user = new
                {
                    response.Data.Client.Name,
                    response.Data.Client.Phone
                },
                table = new
                {
                    response.Data.Table.Number,
                    response.Data.Table.NumberOfSeats,
                    response.Data.Table.Type,
                    response.Data.Table.Price,
                }
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReservationTableCreateRequest request)
    {
        var table = await ReservationTableService.Create(request);

        IBaseResponse<bool> response;

        if (!table)
        {
            response = new BaseResponse<bool>(false, "Ошибка бронирования столика", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] ReservationTableDeleteRequest request)
    {
        var table = await ReservationTableService.Delete(request);

        IBaseResponse<bool> response;

        if (!table)
        {
            response = new BaseResponse<bool>(false, "Ошибка удаления забронированого столика", false);

            return BadRequest(response);
        }

        response = new BaseResponse<bool>(true, null, true);

        return Ok(response);
    }
}