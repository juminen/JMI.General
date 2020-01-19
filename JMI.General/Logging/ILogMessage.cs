using System;

namespace JMI.General.Logging
{
    public interface ILogMessage
    {
        /// <summary>
        /// Message contents
        /// </summary>
        string Message { get; }
        /// <summary>
        /// Message status <see cref="LogMessageStatus"/>
        /// </summary>
        LogMessageStatus Status { get; }
        /// <summary>
        /// Date and time when message was created.
        /// </summary>
        DateTime Time { get; }
    }
}