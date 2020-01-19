using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Data;

namespace JMI.General.Logging
{
    public class Logger
    {
        #region constructors
        public Logger()
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
        protected ObservableCollection<ILogMessage> messages;
        public ListCollectionView Messages { get; private set; }
        /// <summary>
        /// Time format for formatting log message time. Default time format is "yyyy-MM-dd hh:mm:ss.fff".
        /// </summary>
        public string TimeFormat { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Clears all <see cref="Messages"/> and sends event <see cref="MessagesCleared"/>.
        /// </summary>
        public void ClearMessages()
        {
            messages.Clear();
            MessagesCleared?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Creates new log message and logs it using <see cref="Log(ILogMessage)"/>
        /// </summary>
        /// <param name="status">Message status, <see cref="LogMessageStatus"/></param>
        /// <param name="message">Message</param>
        public void Log(LogMessageStatus status, string message)
        {
            ILogMessage msg = new LogMessage(status, message);
            Log(msg);
        }

        /// <summary>
        /// Adds message to collection <see cref="Messages"/> and sends event <see cref="MessageReceived"/>.
        /// </summary>
        /// <param name="message"><see cref="ILogMessage"/></param>
        public void Log(ILogMessage message)
        {
            messages.Add(message);
            LogMessageEventArgs args = new LogMessageEventArgs(message);
            MessageReceived?.Invoke(this, args);
        }

        /// <summary>
        /// Copies message time, status, and contents to clipboard. Values are separated by tab.
        /// </summary>
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
        /// <summary>
        /// Event is fired when messages are cleared, <see cref="ClearMessages"/>
        /// </summary>
        public event EventHandler MessagesCleared;
        /// <summary>
        /// Event is fired when message is received, <see cref="Log(ILogMessage)"/>
        /// </summary>
        public event EventHandler<LogMessageEventArgs> MessageReceived;
        #endregion

        #region event handlers
        #endregion
    }
}
