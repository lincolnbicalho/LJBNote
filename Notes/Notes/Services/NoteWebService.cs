using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Notes.Models;

namespace Notes.Services
{
    public class NoteWebService : INoteWebService
    {
        private readonly INoteService _noteService;
        private readonly ILogger<NoteWebService> _logger;
        private readonly IMapper _mapper;

        public NoteWebService(INoteService noteService, ILogger<NoteWebService> logger, IMapper mapper)
        {
            _noteService = noteService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<NoteModel>> GetAllNotes() 
        { 
            var notes = await _noteService.GetAllNotes();
            return _mapper.Map<List<NoteModel>>(notes);
        }
        public async Task<NoteModel> GetById(int id) 
        {
            var note = await _noteService.GetById(id);
            return _mapper.Map<NoteModel>(note);
        }
        public async Task<NoteModel> Save(NoteModel note) 
        {
            var resultModel = await _noteService.Save(_mapper.Map<Note>(note));
            return _mapper.Map<NoteModel>(resultModel);
        }
        public async Task<bool> Update(NoteModel note) 
        {
            var result = await _noteService.Update(_mapper.Map<Note>(note));
            return result;
        }
        public async Task<bool> UpdateText(int  id, string text) 
        {
            var result = await _noteService.UpdateText(id, text);
            return result;
        }
        public async Task<bool> DeleteById(int id) 
        {
            var result = await _noteService.DeleteById(id);
            return result;
        }
    }
}
