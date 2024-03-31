﻿
using Microsoft.Extensions.Logging;

namespace nk.Logging;

public static class LoggerModule
{
    // PRIVATE

    private static readonly ConsoleLogger _logger = new();

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
