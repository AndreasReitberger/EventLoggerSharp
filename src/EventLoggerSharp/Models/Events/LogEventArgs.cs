using AndreasReitberger.Logging.Enums;
using AndreasReitberger.Logging.Interfaces;
using Newtonsoft.Json;

namespace AndreasReitberger.Logging.Events
{
    public partial class LogEventArgs : EventArgs, ILogEventArgs
    {
        #region Properties
        public string? Message { get; set; }
        public LogLevel Level { get; set; }
        public LogType Type { get; set; }
        public Exception? Exception { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}
