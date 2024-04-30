using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Domain.Entity;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public int Price { get; set; }
    
    public int CatalogId { get; set; }
    public virtual Category Category { get; set; }
}