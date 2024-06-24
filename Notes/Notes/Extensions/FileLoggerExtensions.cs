using Notes.Components.Pages.Category;
using Notes.Helpers;

namespace Notes.Extensions;

public static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath)
    {
        builder.Services.AddSingleton<ILoggerProvider>(provider => new FileLoggerProvider(filePath));
        return builder;
    }
}