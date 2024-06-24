namespace Notes.Helpers;

public class FileLogger : ILogger
{
    private readonly string _filePath;
    private readonly object _lock = new object();

    public FileLogger(string filePath)
    {
        _filePath = filePath;
        EnsureFileExists();
    }

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var logRecord = $"{logLevel.ToString()} {DateTime.Now}: {formatter(state, exception)}";
        lock (_lock)
        {
            File.AppendAllText(_filePath, logRecord + Environment.NewLine);
        }
    }

    private void EnsureFileExists()
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Dispose();
        }
    }
}