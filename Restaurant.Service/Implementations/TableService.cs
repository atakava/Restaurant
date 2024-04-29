using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Table;
using Restaurant.Service.Interfaces;

namespace Restaurant.Service.Implementations;

public class TableService : ITableService
{
    private readonly ITableRepository _repository;

    public TableService(ITableRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Table>> Select()
    {
        return await _repository.Select();
    }

    public async Task<Table?> Get(int id)
    {
        return await _repository.Get(id);
    }

    public async Task<Table?> GetByNumber(int number)
    {
        return await _repository.GetByNumber(number);
    }

    public async Task<Table?> GetByNumberOfSeats(int numberOfSeats)
    {
        return await _repository.GetByNumberOfSeats(numberOfSeats);
    }

    public async Task<bool> Create(TableCreateRequest entity)
    {
        var table = new Table
        {
            Number = entity.Number,
            Type = entity.Type,
            NumberOfSeats = entity.NumberOfSeats,
            Price = entity.Price
        };

        await _repository.Create(table);

        return true;
    }

    public async Task<bool> Update(TableUpdateRequest entity)
    {
        var table = await _repository.Get(entity.Id);

        if (table == null)
            return false;

        table.Number = entity.Number ?? table.Number;
        table.NumberOfSeats = entity.NumberOfSeats ?? table.NumberOfSeats;
        table.Price = entity.Price ?? table.Price;
        table.Type = entity.Type ?? table.Type;

        await _repository.Update(table);

        return true;
    }

    public async Task<bool> Delete(TableDeleteRequest entity)
    {
        var table = await _repository.Get(entity.Id);

        if (table == null)
            return false;

        await _repository.Delete(table);

        return true;
    }
}