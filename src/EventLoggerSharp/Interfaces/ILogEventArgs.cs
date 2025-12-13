using AndreasReitberger.Logging.Enums;

namespace AndreasReitberger.Logging.Interfaces
{
    public interface ILogEventArgs
    {
        #region Propertie
        public string? Message { get; set; }
        public LogLevel Level { get; set; }
        public LogType Type { get; set; }
        public Exception? Exception { get; set; }
        #endregion
    }
}
