using Microsoft.EntityFrameworkCore;
using ToDo.Models.Interfaces;
using ToDo.Models.ModelsDbo;

namespace ToDo.Data.Repositorys
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationContext context;

        public NoteRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<NoteDbo>> GetNotes()
        {
            return await context.Notes.ToListAsync();
        }

        public async Task<ICollection<NoteDbo>> GetNotesByOwnerId(int ownerId)
        {
            return await context.Notes.Where(x => x.OwnerId == ownerId).ToListAsync();
        }

        public async Task<NoteDbo?> GetNoteById(int id)
        {
            return await context.Notes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<NoteDbo?> AddNote(NoteDbo note)
        {
            var createdNote = await context.Notes.AddAsync(note);

            await context.SaveChangesAsync();

            return createdNote.Entity;
        }

        public async Task<NoteDbo?> UpdateNote(NoteDbo note)
        {
            var updatedNote = context.Notes.Update(note);
            await context.SaveChangesAsync();
            return updatedNote.Entity;
        }

        public async Task DeleteNote(NoteDbo note)
        {
            context.Notes.Remove(note);
            await context.SaveChangesAsync();
        }
    }
}
