using ApplicationCore.Entities;
using Notes.Models;

namespace Notes.Services
{
    public interface ICategoryWebService
    {
        Task<bool> DeleteById(int id);
        Task<List<CategoryModel>> GetAllCategories();
        Task<CategoryModel> GetById(int id);
        Task<CategoryModel> Save(CategoryModel note);
        Task<bool> Update(CategoryModel note);
    }
}