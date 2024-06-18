using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.DataAccess;
using System.Data;

namespace Infrastructure.Data;

public class CategoryRepository : ICategoryRepository
{
    private readonly ISqlDataAccess _dataAccess;

    public CategoryRepository(ISqlDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public async Task<List<Category>> ListAll()
    {
        var result = await _dataAccess.LoadData<Category,
            dynamic>("SELECT * FROM Category", new { }, CommandType.Text);
        return result.ToList<Category>();
    }
    public async Task<Category> GetById(int id)
    {
        var result = await _dataAccess.LoadData<Category,
            dynamic>("SELECT * FROM Category WHERE Id = @Id", new { Id = id }, CommandType.Text);
        return result.FirstOrDefault();
    }
    public async Task<Category> Create(Category entity)
    {
        var result = await _dataAccess.LoadData<Category,
            dynamic>("INSERT INTO dbo.Category ([Title], [Description]) VALUES (@Title, @Description)",
            new { Title = entity.Title, Description = entity.Description }, CommandType.Text);
        return result.FirstOrDefault();
    }
    public async Task<bool> Update(Category entity)
    {
        await _dataAccess.SaveData<Category>(
            "UPDATE Category SET [Title] = @Title, [Description] = @Description WHERE Id = @Id",
            entity,
            CommandType.Text);
        return true;
    }
    public async Task<bool> Delete(Category entity)
    {
        await _dataAccess.SaveData("DELETE FROM Category WHERE Id = @Id", new { Id = entity.Id }, CommandType.Text);
        return true;
    }
}
