using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IImageService
    {
        bool DeleteById(int id);
        List<Image> GetAllImagesByNoteId(int noteId);
        Image GetById(int id);
        Image Save(Image note);
        bool Update(Image note);
    }
}