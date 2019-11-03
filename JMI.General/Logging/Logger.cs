using System;
using System.Collections.ObjectModel;
using System.Text;
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
            TimeFormat = $"yyyy-MM-dd hh:mm:ss.fff";
        }
        #endregion

        #region properties
        private static readonly Lazy<Logger> lazy = new Lazy<Logger>(() => new Logger());
        public static Logger Instance { get { return lazy.Value; } }
        private ObservableCollection<ILogMessage> messages;
        public ListCollectionView Messages { get; private set; }
        /// <summary>
        /// Default time format is "yyyy-MM-dd hh:mm:ss.fff".
        /// </summary>
        public string TimeFormat { get; set; }
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

        public void CopyToClipboard()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TIME\tSTATUS\tMESSAGE");
            foreach (ILogMessage item in messages)
            {
                sb.AppendLine($"{item.Time.ToString(TimeFormat)}\t{item.Status.DisplayText}\t{item.Message}");
            }
            System.Windows.Clipboard.SetText(sb.ToString());
        }

        //TODO: export messages to file
        #endregion

        #region events
        public event EventHandler MessagesCleared;
        public event EventHandler<LogMessageEventArgs> MessageReceived;
        #endregion

        #region event handlers
        #endregion
    }
}
