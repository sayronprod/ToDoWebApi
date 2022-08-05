using ToDo.Models.ModelsDbo;

namespace ToDo.Models.Interfaces
{
    public interface INoteRepository
    {
        public Task<ICollection<NoteDbo>> GetNotes();
        public Task<ICollection<NoteDbo>> GetNotesByOwnerId(int ownerId);
        public Task<NoteDbo?> GetNoteById(int id);
        public Task<NoteDbo?> AddNote(NoteDbo note);
        public Task<NoteDbo?> UpdateNote(NoteDbo note);
        public Task DeleteNote(NoteDbo note);
    }
}
