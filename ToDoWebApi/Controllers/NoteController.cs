using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Models.Interfaces;
using ToDo.Models.ModelsDto;

namespace ToDo.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NoteController : BaseApiController
    {
        private readonly ILogger<NoteController> logger;
        private readonly INoteService noteService;
        public int UserId
        {
            get
            {
                string? id = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
                int userId;
                int.TryParse(id, out userId);
                return userId;
            }
        }

        public NoteController(ILogger<NoteController> logger, INoteService noteService)
        {
            this.logger = logger;
            this.noteService = noteService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<MyNote>))]
        public async Task<ICollection<MyNote>> GetMyNotes()
        {
            var myNotes = await noteService.GetMyNotes(UserId);
            return myNotes;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(MyNote))]
        [ProducesResponseType(404, Type = typeof(MessageDto))]
        public async Task<object> GetMyNoteById(int id)
        {
            object result;

            var myNote = await noteService.GetMyNoteById(UserId, id);
            if (myNote == null)
            {
                result = new MessageDto
                {
                    Message = "Note not found"
                };
                SetStatusCode(404);
            }
            else
            {
                result = myNote;
                SetStatusCode(200);
            }

            return result;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(MyNote))]
        public async Task<object?> AddMyNote(AddNoteRequestDto request)
        {
            object? result;

            var newNote = await noteService.AddNote(UserId, request.NoteText);
            SetStatusCode(200);
            result = newNote;

            return result;
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(MyNote))]
        [ProducesResponseType(404, Type = typeof(MessageDto))]
        public async Task<object> UpdateMyNote(UpdateNoteRequestDto request)
        {
            object? result;
            var updatedNote = await noteService.UpdateNote(UserId, request.NoteId, request.NoteText);
            if (updatedNote is null)
            {
                result = new MessageDto
                {
                    Message = "Note not Found"
                };
                SetStatusCode(404);
            }
            else
            {
                result = updatedNote;
                SetStatusCode(200);
            }

            return result;
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(MessageDto))]
        [ProducesResponseType(404, Type = typeof(MessageDto))]
        public async Task<object> DeleteMyNote(int noteId)
        {
            object? result;

            var deleteResult = await noteService.DeleteNote(UserId, noteId);
            if (deleteResult)
            {
                result = new MessageDto
                {
                    Message = "Success"
                };
                SetStatusCode(200);
            }
            else
            {
                result = new MessageDto
                {
                    Message = "Note not found"
                };
                SetStatusCode(404);
            }

            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Note>))]
        public async Task<ICollection<Note>> GetAllNotes()
        {
            var notes = await noteService.GetAllNotes();
            return notes;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Note))]
        [ProducesResponseType(404, Type = typeof(MessageDto))]
        public async Task<object> GetNoteById(int id)
        {
            object result;

            var note = await noteService.GetNoteById(id);
            if (note == null)
            {
                result = new MessageDto
                {
                    Message = "Note not found"
                };
                SetStatusCode(404);
            }
            else
            {
                result = note;
                SetStatusCode(200);
            }

            return result;
        }
    }
}
