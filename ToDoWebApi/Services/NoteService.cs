using AutoMapper;
using ToDo.Models;
using ToDo.Models.Interfaces;
using ToDo.Models.ModelsDbo;
using ToDo.Models.ModelsDto;

namespace ToDo.WebApi.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository noteRepository;
        private readonly IMapper mapper;

        public NoteService(INoteRepository noteRepository, IMapper mapper)
        {
            this.noteRepository = noteRepository;
            this.mapper = mapper;
        }

        public async Task<ICollection<MyNote>> GetMyNotes(int userId)
        {
            var notes = await noteRepository.GetNotesByOwnerId(userId);
            ICollection<MyNote> result = mapper.Map<ICollection<MyNote>>(notes);
            return result;
        }

        public async Task<MyNote?> GetMyNoteById(int userId, int noteId)
        {
            MyNote? result = null;

            var note = await noteRepository.GetNoteById(noteId);
            if (note is not null && note.OwnerId == userId)
            {
                result = mapper.Map<MyNote>(note);
            }

            return result;
        }

        public async Task<MyNote?> AddNote(int userId, string noteText)
        {
            MyNote? createdNote = null;

            NoteDbo newNote = new NoteDbo
            {
                OwnerId = userId,
                NoteText = noteText,
                Created = DateTime.Now,
                Updated = null
            };

            var noteCreatedResult = await noteRepository.AddNote(newNote);
            createdNote = mapper.Map<MyNote>(noteCreatedResult);

            return createdNote;
        }

        public async Task<MyNote?> UpdateNote(int userId, int noteId, string newNoteText)
        {
            MyNote? result = null;

            var note = await noteRepository.GetNoteById(noteId);
            if (note is not null && note.OwnerId == userId)
            {
                note.NoteText = newNoteText;
                note.Updated = DateTime.Now;
                var updatedNote = await noteRepository.UpdateNote(note);
                result = mapper.Map<MyNote>(updatedNote);
            }

            return result;
        }

        public async Task<bool> DeleteNote(int userId, int noteId)
        {
            bool result = false;

            var note = await noteRepository.GetNoteById(noteId);
            if (note is not null && note.OwnerId == userId)
            {
                await noteRepository.DeleteNote(note);
                result = true;
            }

            return result;
        }

        public async Task<ICollection<Note>> GetAllNotes()
        {
            var notes = await noteRepository.GetNotes();
            ICollection<Note> result = mapper.Map<ICollection<Note>>(notes);
            return result;
        }

        public async Task<Note?> GetNoteById(int noteId)
        {
            Note? result = null;

            var note = await noteRepository.GetNoteById(noteId);
            if (note is not null)
            {
                result = mapper.Map<Note>(note);
            }

            return result;
        }
    }
}
