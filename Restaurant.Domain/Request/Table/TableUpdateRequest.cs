using Restaurant.Domain.Enum;

namespace Restaurant.Domain.Request.Table;

public class TableUpdateRequest
{
    public int Id { get; set; }
    public int? Number { get; set; }
    public TableType? Type { get; set; }
    public int? NumberOfSeats { get; set; }
    public int? Price { get; set; }
}