using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Category: BaseEntity
{
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }
}
