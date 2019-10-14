namespace JMI.General.Logging
{
    public class LogMessageStatus
    {
        protected LogMessageStatus(string displayText)
        {
            DisplayText = displayText;
        }

        public string DisplayText { get; }

        public static LogMessageStatus Error { get; } = new LogMessageStatus("Error");
        public static LogMessageStatus Normal { get; } = new LogMessageStatus("Normal");
        public static LogMessageStatus Warning { get; } = new LogMessageStatus("Warning");
    }
}
