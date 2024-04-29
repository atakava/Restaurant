namespace Restaurant.Domain.Request.ReservationTable;

public class CheckTableReservationRequest
{
    //int number, DateTime timeStart, DateTime timeEnd
    public int Number { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
}