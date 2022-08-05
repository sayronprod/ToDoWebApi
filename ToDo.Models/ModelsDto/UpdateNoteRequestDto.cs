using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.ModelsDto
{
    public class UpdateNoteRequestDto
    {
        [Required]
        public int NoteId { get; set; }
        [Required]
        [StringLength(2000, MinimumLength = 1)]
        public string NoteText { get; set; }
    }
}
