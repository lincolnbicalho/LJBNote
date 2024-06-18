using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Notes.Models;

namespace Notes.Services;

public class CategoryWebService : ICategoryWebService
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoryWebService(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }
    public async Task<List<CategoryModel>> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategories();
        return _mapper.Map<List<CategoryModel>>(categories);
    }
    public async Task<CategoryModel> GetById(int id) 
    { 
        var category = await _categoryService.GetById(id);
        return _mapper.Map<CategoryModel>(category);
    }
    public async Task<CategoryModel> Save(CategoryModel note) 
    {
        var toSaveModel = _mapper.Map<Category>(note);
        var category = await _categoryService.Save(toSaveModel);
        return _mapper.Map<CategoryModel>(category);
    }
    public async Task<bool> Update(CategoryModel note) 
    {
        var toUpdateModel = _mapper.Map<Category>(note);
        bool result = await _categoryService.Update(toUpdateModel);
        return result;
    }
    public async Task<bool> DeleteById(int id) 
    {
        bool result = await _categoryService.DeleteById(id);
        return result;
    }
}
