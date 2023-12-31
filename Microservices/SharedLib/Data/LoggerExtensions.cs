﻿using Microsoft.Extensions.Logging;

namespace SharedLib.Data;

public static partial class LoggerExtensions
{
    [LoggerMessage(LogLevel.Debug, "Executing \"{Sql}\" with param: {Param}")]
    public static partial void LogSql(this ILogger logger, string sql,
        object param);
}