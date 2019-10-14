using System;

namespace JMI.General.Logging
{
    public class LogMessageEventArgs : EventArgs
    {
        public LogMessageEventArgs(ILogMessage message)
        {
            Message = message;
        }
        public ILogMessage Message { get; }
    }
}