using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class CustomerRegistrationForm
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;
}
