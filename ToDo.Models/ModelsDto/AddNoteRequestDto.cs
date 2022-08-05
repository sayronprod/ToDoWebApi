using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.ModelsDto
{
    public class AddNoteRequestDto
    {
        [Required]
        [StringLength(2000, MinimumLength = 1)]
        public string NoteText { get; set; }
    }
}
