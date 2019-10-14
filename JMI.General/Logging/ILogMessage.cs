using System;

namespace JMI.General.Logging
{
    public interface ILogMessage
    {
        string Message { get; }
        LogMessageStatus Status { get; }
        DateTime Time { get; }
    }
}