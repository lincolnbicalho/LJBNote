using Notes.Models;

namespace Notes.Services
{
    public interface INoteWebService
    {
        Task<bool> DeleteById(int id);
        Task<List<NoteModel>> GetAllNotes();
        Task<NoteModel> GetById(int id);
        Task<NoteModel> Save(NoteModel note);
        Task<bool> Update(NoteModel note);
        Task<bool> UpdateText(int id, string text);
    }
}