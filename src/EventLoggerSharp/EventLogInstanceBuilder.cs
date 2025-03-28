namespace AndreasReitberger.Logging
{
    public partial class EventLogger
    {
        public class EventLogInstanceBuilder
        {
            #region Instance
            readonly EventLogger _logger = new();
            #endregion

            #region Methods

            public EventLogger Build()
            {
                return _logger;
            }

            public EventLogInstanceBuilder WithFilePath(string logFilePath)
            {
                _logger.LogPath = logFilePath;
                return this;
            }

            public EventLogInstanceBuilder WithFileExtension(string extension)
            {
                _logger.LogFileExtension = extension;
                return this;
            }
            #endregion
        }
    }
}
