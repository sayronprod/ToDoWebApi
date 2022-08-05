using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Models.ModelsDbo
{
    public class NoteDbo
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string NoteText { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        [ForeignKey("OwnerId")]
        public UserDbo Owner { get; set; }
    }
}
