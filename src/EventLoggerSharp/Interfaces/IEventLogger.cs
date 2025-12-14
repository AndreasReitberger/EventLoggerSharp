using AndreasReitberger.Logging.Enums;
using System.ComponentModel;
using System.Threading.Tasks;

namespace AndreasReitberger.Logging.Interfaces
{
    public interface IEventLogger : INotifyPropertyChanged, IDisposable, ICloneable
    {
        #region Properties

        #region Instance
        public Guid Id { get; set; }
#if !NETFRAMEWORK
        public static IEventLogger? Instance { get; set; }
#endif
        #endregion

        #region States
        public bool UseJson { get; set; }
        #endregion

        #region Logging
        public LogLevel LogLevel { get; set; }
        public string LogPath { get; set; }
        public string LogFileExtension { get; set; }

        #endregion

        #endregion

        #region EventHandlers

        public event EventHandler? Error;
        #endregion

        #region Methods

        #region Logging
        public void Log(string message, LogType level = LogType.Info);
        public Task LogAsync(string message, LogType level = LogType.Info);
        #endregion

        #endregion
    }
}
