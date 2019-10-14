using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace JMI.General.Logging
{
    public sealed class Logger
    {
        #region constructors
        private Logger()
        {
            messages = new ObservableCollection<ILogMessage>();
            Messages = new ListCollectionView(messages)
            {
                IsLiveSorting = true
            };
            Messages.LiveSortingProperties.Add(nameof(ILogMessage.Time));
        }
        #endregion

        #region properties
        private static readonly Lazy<Logger> lazy = new Lazy<Logger>(() => new Logger());
        public static Logger Instance { get { return lazy.Value; } }
        private ObservableCollection<ILogMessage> messages;
        public ListCollectionView Messages { get; private set; }
        #endregion

        #region methods
        public void ClearMessages()
        {
            messages.Clear();
            MessagesCleared?.Invoke(this, EventArgs.Empty);
        }

        public void Log(LogMessageStatus status, string message)
        {
            ILogMessage msg = new LogMessage(status, message);
            Log(msg);
        }

        public void Log(ILogMessage message)
        {
            messages.Add(message);
            LogMessageEventArgs args = new LogMessageEventArgs(message);
            MessageReceived?.Invoke(this, args);
        }
        #endregion

        #region events
        public event EventHandler MessagesCleared;
        public event EventHandler<LogMessageEventArgs> MessageReceived;
        #endregion

        #region event handlers
        #endregion
    }
}
