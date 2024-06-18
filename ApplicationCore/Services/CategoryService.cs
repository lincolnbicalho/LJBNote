using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CommunityToolkit.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace ApplicationCore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await _categoryRepository.ListAll();
        }
        public async Task<Category> GetById(int id)
        {
            Guard.IsGreaterThan(id, 0, "The Id is wrong");
            var category = await _categoryRepository.GetById(id);
            Guard.IsNotNull(category, $"There is no Category for this ID {id}.");
            return category;
        }
        public async Task<Category> Save(Category category)
        {
            Guard.IsNotNull<Category>(category, "The Category must not be null.");
            Guard.IsNotNullOrWhiteSpace(category.Title, "The Title must not be null.");
            return await _categoryRepository.Create(category);
        }
        public async Task<bool> Update(Category category)
        {
            Guard.IsNotNull<Category>(category, "The Category must not be null.");
            Guard.IsGreaterThan(category.Id, 0, $"The Id {category.Id} must be valid.");
            Guard.IsNotNullOrWhiteSpace(category.Title, "The title must not be null.");
            var result = await _categoryRepository.Update(category);
            Guard.IsFalse(result, "It's not possible to delete this item");
            return true;
        }
        public async Task<bool> DeleteById(int id)
        {
            Guard.IsGreaterThan(id, 0, $"The Id must be valid, Id: {id}");
            Category category = await _categoryRepository.GetById(id);
            Guard.IsNotNull<Category>(category, "The Category must not be null.");
            var result = await _categoryRepository.Delete(category);
            Guard.IsFalse(result, "It's not possible to delete this item");
            return true;
        }
    }
}
