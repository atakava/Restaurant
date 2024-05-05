namespace Restaurant.Domain.Request.ReservationTable;

public class ReservationTableCreateRequest
{
    public int ClientId { get; set; }

    public int TableId { get; set; }
    public int TableNumber { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    
}