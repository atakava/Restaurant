using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Domain.Entity;

public class ReservationTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ClientId { get; set; }
    public virtual Client Client { get; set; }

    public int TableId { get; set; }
    public virtual Table Table { get; set; }

    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}