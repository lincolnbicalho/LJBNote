using Notes.Models;

namespace Notes.Services
{
    public interface IImageOCRNoteService
    {
        int NumberOfImagesProcessed(int NoteId);
        int NumberOfImagestoProcess(int NoteId);
        Task<List<ImageNoteModel>> ReadImages(int NoteId);
    }
}