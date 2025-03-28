using AndreasReitberger.Logging.Enums;
using AndreasReitberger.Logging.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AndreasReitberger.Logging
{
    public partial class EventLogger : ObservableObject, IEventLogger
    {

        #region Instance

        static EventLogger? _instance = null;
        static readonly object Lock = new();
        public static EventLogger Instance
        {
            get
            {
                lock (Lock)
                {
                    _instance ??= new EventLogger();
                }
                return _instance;
            }
            set
            {
                if (_instance == value) return;
                lock (Lock)
                {
                    _instance = value;
                }
            }
        }

        [ObservableProperty]
        public partial Guid Id { get; set; } = Guid.Empty;

        #endregion

        #region Properties

        #region General

        [ObservableProperty]
        public partial bool UseJson { get; set; } = true;

        [ObservableProperty]
        public partial string LogPath { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string LogFileExtension { get; set; } = ".log";

        #endregion

        #endregion

        #region Constructor
        public EventLogger()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Public Methods

        #region Logging
        public void WriteToConsole(string message, LogLevel level = LogLevel.Info)
        {
            ConsoleColor foreground = level switch
            {
                LogLevel.Warning => ConsoleColor.Yellow,
                LogLevel.Error => ConsoleColor.Red,
                _ => ConsoleColor.White,
            };
            if (!string.IsNullOrEmpty(message))
            {
                Console.ForegroundColor = foreground;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public void Log(string message, LogLevel level = LogLevel.Info)
        {
            string logText = $"{DateTime.Now}: {level}\t| {message}";
            // Always report the text also to the console
            WriteToConsole(logText, level);

            if (string.IsNullOrEmpty(LogPath) || string.IsNullOrEmpty(LogFileExtension)) 
            {
                Debug.WriteLine(logText);
            }
            else
            {
                string filename = $"{DateTime.Now:yyyy-MM-dd}{(LogFileExtension.StartsWith(".") ? LogFileExtension : $".{LogFileExtension}")}";
                string targetFile = Path.Combine(LogPath, filename);

                if (!Directory.Exists(LogPath)) Directory.CreateDirectory(LogPath);
                using FileStream fs = new(targetFile, FileMode.Append, FileAccess.Write);
                using StreamWriter sw = new(fs);

                sw.WriteLine(logText);
                sw.Close();
            }
        }
        public async Task LogAsync(string message, LogLevel level = LogLevel.Info)
        {
            string logText = $"{DateTime.Now}: {level} | {message}";
            // Always report the text also to the console
            WriteToConsole(logText, level);

            if (string.IsNullOrEmpty(LogPath) || string.IsNullOrEmpty(LogFileExtension)) 
            {
                Debug.WriteLine(logText);
            }
            else
            {
                string filename = $"{DateTime.Now:yyyy-MM-dd}{(LogFileExtension.StartsWith(".") ? LogFileExtension : $".{LogFileExtension}")}";
                string targetFile = Path.Combine(LogPath, filename);

                if (!Directory.Exists(LogPath)) Directory.CreateDirectory(LogPath);
                using FileStream fs = new(targetFile, FileMode.Append, FileAccess.Write);
                using StreamWriter sw = new(fs);

                await sw.WriteLineAsync(logText);
                sw.Close();
            }
        }
        #endregion

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not EventLogger item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            // Ordinarily, we release unmanaged resources here;
            // but all are wrapped by safe handles.
            // Release disposable objects.
            if (disposing)
            {

            }
        }
        #endregion

        #region Clone

        public object Clone() => MemberwiseClone();
        
        #endregion
    }
}
