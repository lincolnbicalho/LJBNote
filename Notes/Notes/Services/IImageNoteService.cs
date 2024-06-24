using Notes.Models;

namespace Notes.Services
{
    public interface IImageNoteService
    {
        int NumberOfImagesProcessed(int NoteId);
        int NumberOfImagestoProcess(int NoteId);
        Task<List<ImageNoteModel>> ReadImages(int NoteId);
    }
}