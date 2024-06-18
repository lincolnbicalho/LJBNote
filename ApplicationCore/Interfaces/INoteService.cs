using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface INoteService
    {
        Task<bool> DeleteById(int id);
        Task<List<Note>> GetAllNotes();
        Task<Note> GetById(int id);
        Task<Note> Save(Note note);
        Task<bool> Update(Note note);
        Task<bool> UpdateText(int Id, string Text);
    }
}