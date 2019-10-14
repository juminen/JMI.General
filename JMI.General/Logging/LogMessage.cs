using System;
using System.Threading;

namespace JMI.General.Logging
{
    class LogMessage : ILogMessage
    {
        public LogMessage(LogMessageStatus status, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message), "Message can not be empty.");
            }
            Message = message;
            Time = DateTime.Now;
            Status = status;
        }

        public LogMessage(LogMessageStatus status, string message, DateTime time) : this(status, message)
        {
            Time = time;
        }

        public LogMessageStatus Status { get; private set; }
        public DateTime Time { get; private set; }
        public string Message { get; private set; }

        public override string ToString()
        {
            return $"{ Time.ToString("yyyy-MM-dd HH:mm:ss.fff") } - { Status.DisplayText } - { Message }";
        }
    }
}
