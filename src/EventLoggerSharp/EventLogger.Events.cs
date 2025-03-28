using AndreasReitberger.Logging.Events;
using AndreasReitberger.Logging.Interfaces;

namespace AndreasReitberger.Logging
{
    public partial class EventLogger : ObservableObject, IEventLogger
    {

        #region EventHandlers
        public event EventHandler? Error;
        protected virtual void OnError()
        {
            Error?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnError(UnhandledExceptionEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        protected virtual void OnError(JsonConvertEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        #endregion

    }
}
