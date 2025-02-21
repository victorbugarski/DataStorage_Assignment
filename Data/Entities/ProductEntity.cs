using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string? ProductDescription { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

}