using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
    public class NoteModel
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Resume { get; set; }
        public CategoryModel Category { get; set; } = new();
    }
}
