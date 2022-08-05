namespace ToDo.Models.ModelsDto
{
    public class MyNote
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
