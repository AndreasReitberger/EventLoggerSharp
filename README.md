# EventLoggerSharp
An offline C# event logger for .NET applications.

# Usage

```cs
logger = new EventLogger.EventLogInstanceBuilder()
                .WithFilePath(@"C:\temp\Log")
                .WithFileExtension(".dat")
                .Build();
logger.Error += (sender, args) =>
{
    Debug.Write($"Error: {args?.ToString()}");
};

// Log an error
logger.Log($"API: Could not open a connection to {Host}:{Port}", LogLevel.Error);
```