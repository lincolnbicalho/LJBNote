using ApplicationCore.Entities;
using ApplicationCore.Services;
using AutoMapper;
using Notes.Models;

namespace Notes;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<Category, CategoryModel>().ReverseMap();
        CreateMap<Note, NoteModel>().ReverseMap();
    }
}
