using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Restaurant.Domain.Enum;

namespace Restaurant.Domain.Entity;

public class Table
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int Number { get; set; }
    public TableType Type { get; set; }
    public int NumberOfSeats { get; set; }
    public int Price { get; set; }
}