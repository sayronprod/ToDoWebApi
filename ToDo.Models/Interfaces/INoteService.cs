using ToDo.Models.ModelsDto;

namespace ToDo.Models.Interfaces
{
    public interface INoteService
    {
        public Task<ICollection<MyNote>> GetMyNotes(int userId);
        public Task<MyNote?> GetMyNoteById(int userId, int noteId);
        public Task<MyNote?> AddNote(int userId, string noteText);
        public Task<MyNote?> UpdateNote(int userId, int noteId, string newNoteText);
        public Task<bool> DeleteNote(int userId, int noteId);
        public Task<ICollection<Note>> GetAllNotes();
        public Task<Note?> GetNoteById(int noteId);
    }
}
