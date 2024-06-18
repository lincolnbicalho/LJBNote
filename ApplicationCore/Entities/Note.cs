using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Note: BaseEntity
{
    [Required]
    public required string Title { get; set; }
    public string? Text { get; set; }
    public required Category Category { get; set; }
}
