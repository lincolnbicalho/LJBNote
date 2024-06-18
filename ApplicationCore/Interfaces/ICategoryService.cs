using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> DeleteById(int id);
        Task<List<Category>> GetAllCategories();
        Task<Category> GetById(int id);
        Task<Category> Save(Category category);
        Task<bool> Update(Category category);
    }
}