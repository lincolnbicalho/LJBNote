using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CommunityToolkit.Diagnostics;

namespace ApplicationCore.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    private readonly ICategoryRepository _categoryRepository;

    public NoteService(INoteRepository noteRepository, ICategoryRepository categoryRepository)
    {
        _noteRepository = noteRepository;
        _categoryRepository = categoryRepository;
    }
    public async Task<List<Note>> GetAllNotes()
    {
        var notes = await _noteRepository.ListAll();
        return notes;
    }
    public async Task<Note> GetById(int id)
    {
        Guard.IsGreaterThan(id, 0, "The Id is wrong");
        var note = await _noteRepository.GetById(id);
        Guard.IsNotNull(note, $"There is no Note for this ID {id}.");
        return note;
    }
    public async Task<Note> Save(Note note)
    {
        Guard.IsNotNull<Note>(note, "The Note must not be null.");
        Guard.IsNotNullOrWhiteSpace(note.Title, "The Title must not be null.");
        var category = await _categoryRepository.GetById(note.Category.Id);
        Guard.IsNotNull(category, $"There is no Category for this ID {category.Id}.");
        note.Category = category;
        var result = await _noteRepository.Create(note);
        return result;
    }
    public async Task<bool> Update(Note note)
    {
        Guard.IsNotNull<Note>(note, "The Note must not be null.");
        Guard.IsGreaterThan(note.Id, 0, $"The Id {note.Id} must be valid.");
        var noteDB = await _noteRepository.GetById(note.Id);
        Guard.IsNotNull<Note>(noteDB, "The Note doesn't exist");
        Guard.IsNotNullOrWhiteSpace(note.Title, "The Title must not be null.");
        Guard.IsGreaterThan(note.Id, 0, "The Category must be valid.");
        var category = await _categoryRepository.GetById(note.Id);
        Guard.IsNotNull(category, $"There is no Category for the ID {note.Category.Id}.");

        await _noteRepository.Update(note);
        return true;
    }

    public async Task<bool> UpdateText(int Id, string Text)
    {
        Guard.IsGreaterThan(Id, 0, $"The Id {Id} must be valid.");
        Guard.IsNotNullOrWhiteSpace(Text, "The Text must not be empty or null.");
        var note = await _noteRepository.GetById(Id);
        Guard.IsNotNull<Note>(note, "The Note doesn't exist");
        var result = await _noteRepository.UpdateText(Id, Text);
        Guard.IsFalse(result, "It's not possible to delete this item");
        return true;
    }
    public async Task<bool> DeleteById(int id) 
    {
        Guard.IsGreaterThan(id, 0, $"The Id must be valid, Id: {id}");
        var note = await _noteRepository.GetById(id);
        Guard.IsNotNull<Note>(note, "The Note must not be null");
        var result = await _noteRepository.Delete(note);
        Guard.IsFalse(result, "It's not possible to delete this item");
        return true;
    }
}
