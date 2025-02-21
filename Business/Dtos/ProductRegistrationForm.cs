using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProductRegistrationForm
{
    [Required]
    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    [Required]
    public decimal Price { get; set; }
}
