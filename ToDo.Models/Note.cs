namespace ToDo.Models
{
    public class Note
    {
        public int Id { get; set; }
        public User Owner { get; set; }
        public string NoteText { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
