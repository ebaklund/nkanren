
using Microsoft.Extensions.Logging;

namespace nk.Utils;


internal class ConsoleLogger : ILogger
{
    // PRIVATE

    int _logLevel = (int) LogLevel.Error;

    // PUBLIC

    public void SetLogLevel(LogLevel logLevel)
    {
        _logLevel = (int) logLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        if (_logLevel == (int) LogLevel.None)
            return false;

        if (_logLevel > (int) logLevel)
            return false;

        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? ex, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var oldFgColor = Console.ForegroundColor;

        Console.ForegroundColor = logLevel switch
        {
            LogLevel.Debug =>  ConsoleColor.Cyan,
            LogLevel.Information => ConsoleColor.Green,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Critical => ConsoleColor.Red,
            _ => ConsoleColor.White
        };

        var indent = logLevel == LogLevel.Debug ? "    " : " ";
        var msg = formatter(state, ex);

        Console.WriteLine($"{indent}{msg}");
        Console.ForegroundColor = oldFgColor;
    }
}

public static class LoggerModule
{
    // PRIVATE

    private static readonly ConsoleLogger _logger = new ();

    // PUBLIC

    public static void SetLogLevel(LogLevel logLevel)
    {
        _logger.SetLogLevel(logLevel);
    }

    public static void LogDebug(string message)
    {
        _logger.LogDebug(message);
    }

    public static void LogInformation(string message)
    {
        _logger.LogInformation(message);
    }
}


