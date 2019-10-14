namespace JMI.General.Logging
{
    public class LogFactory
    {
        public static ILogMessage CreateNormalMessage(string messageContent)
        {
            return new LogMessage(LogMessageStatus.Normal, messageContent);
        }

        public static ILogMessage CreateWarningMessage(string messageContent)
        {
            return new LogMessage(LogMessageStatus.Warning, messageContent);
        }

        public static ILogMessage CreateErrorMessage(string messageContent)
        {
            return new LogMessage(LogMessageStatus.Error, messageContent);
        }
    }
}
