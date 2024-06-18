using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Image: BaseEntity
{
    [Required]
    public required string Path { get; set; }
    [Required]
    public required string ORCText { get; set; }
    [Required]
    public DateTime TimeStamp { get; set; }
    [Required]
    public required Note Note { get; set; }
}
