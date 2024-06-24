using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.DataAccess;
using System.Data;

namespace Infrastructure.Data;

public class NoteRepository : INoteRepository
{
    private readonly ISqlDataAccess _dataAccess;

    public NoteRepository(ISqlDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public async Task<List<Note>> ListAll()
    {
        string sql = @"SELECT * FROM dbo.Note N 
                       JOIN dbo.Category C ON (N.CategoryId = C.Id)";

        Func<Note, Category, Note> map = (note, category) =>
        {
            note.Category = category;
            return note;
        };

        var result = await _dataAccess.LoadData(sql, new { }, CommandType.Text, map);

        return result.ToList<Note>();
    }
    public async Task<Note> GetById(int id)
    {
        string sql = @"SELECT * FROM dbo.Note N 
                       JOIN dbo.Category C ON (N.CategoryId = C.Id)
                       WHERE N.Id = @Id";

        Func<Note, Category, Note> map = (note, category) =>
        {
            note.Category = category;
            return note;
        };

        var result = await _dataAccess.LoadData(sql, new { Id = id }, CommandType.Text, map);
        return result.FirstOrDefault();
    }
    public async Task<Note> Create(Note entity)
    {
        var result = await _dataAccess.LoadData<Note, dynamic>(@"INSERT INTO dbo.Note([Title],[Text],[CategoryId],[TimeStamp]) VALUES (@Title,@Text,@CategoryId,GETDATE());
                                                                SELECT * FROM dbo.Note WHERE Id = SCOPE_IDENTITY();",
                                                                new { entity.Title, entity.Text, CategoryId = entity.Category.Id }, CommandType.Text);
        return result.FirstOrDefault();
    }
    public async Task<bool> Update(Note entity)
    {
        await _dataAccess.SaveData<dynamic>("UPDATE dbo.Note SET [Title] = @Title, [Text] = @Text, [Resume] = @Resume, [CategoryId] = @CategoryId WHERE Id = @Id",
                                    new { entity.Id, entity.Title, entity.Text, entity.Resume ,CategoryId = entity.Category.Id }, CommandType.Text);
        return true;
    }

    public async Task<bool> UpdateText(int Id, string Text)
    {
        await _dataAccess.SaveData<dynamic>("UPDATE dbo.Note SET Text = @Text WHERE Id = @Id", new { Id, Text }, CommandType.Text);
        return true;
    }
    public async Task<bool> Delete(Note entity)
    {
        await _dataAccess.SaveData("DELETE dbo.Note WHERE Id = @Id",
                                    new { entity.Id }, CommandType.Text);
        return true;
    }
}
