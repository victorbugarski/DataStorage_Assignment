using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProjectRegistrationForm
{
    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int StatusId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int ProductId { get; set; }

}