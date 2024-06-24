using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Notes.Models;
using OpenAI_API.Models;
using OpenAI_API;

namespace Notes.Services
{
    public class NoteWebService : INoteWebService
    {
        private readonly INoteService _noteService;
        private readonly ILogger<NoteWebService> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly string? _key;
        public NoteWebService(INoteService noteService, ILogger<NoteWebService> logger, IMapper mapper, IConfiguration config)
        {
            _noteService = noteService;
            _logger = logger;
            _mapper = mapper;
            _config = config;
            _key = _config.GetValue<string>("ImageUpload:SecretKeyApi");
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
            note.Resume = await AIResumeText(note);
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
        private async Task<string> AIResumeText(NoteModel note)
        {
            if (!string.IsNullOrEmpty(note.Text))
            {
                OpenAIAPI api = new OpenAIAPI(_key);
                var chat = api.Chat.CreateConversation();

                chat.Model = new Model("gpt-4o");
                chat.AppendSystemMessage("You are a Content Writer, Technical Writer. you will Focuses on summarizing the note text. ");
                chat.AppendUserInput("The maximum lenth is 100 characters.");
                chat.AppendUserInput("For example: 'The Creative Act, is telling how to improve yourself by focusing on your silence your mind, paying atention on your eviroment...'.");
                chat.AppendUserInput("Give me the summary for this note text.");
                //chat.AppendUserInput($"{note.Title}.");
                chat.AppendUserInput($"This is text to summarize : {note.Text}");
                string response = await chat.GetResponseFromChatbotAsync();
                return response;
            }
            return string.Empty;
        }
    }
}
