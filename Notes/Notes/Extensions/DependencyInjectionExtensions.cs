using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.DataAccess;
using Notes.Services;
using Radzen;

namespace Notes.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
        builder.Services.AddSingleton<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<ICategoryWebService, CategoryWebService>();

        builder.Services.AddSingleton<INoteRepository, NoteRepository>();
        builder.Services.AddSingleton<INoteService, NoteService>();
        builder.Services.AddSingleton<INoteWebService, NoteWebService>();

        builder.Services.AddSingleton<IImageService, ImageService>();     

        builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();

        builder.Services.AddScoped<DialogService>();
        builder.Services.AddScoped<NotificationService>();
        builder.Services.AddScoped<IImageNoteService, ImageNoteService>();


    }

    public static void AddAutoMapperService(this WebApplicationBuilder builder) 
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        IMapper mapper = mapperConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);
    }
}
