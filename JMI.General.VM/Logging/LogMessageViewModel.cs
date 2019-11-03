using JMI.General.Logging;

namespace JMI.General.VM.Logging
{
    public class LogMessageViewModel //: SelectionCollectionItem
    {
        public LogMessageViewModel(ILogMessage message, string timeFormat)
        {
            msg = message;
            this.timeFormat = timeFormat;
        }

        private ILogMessage msg;
        private readonly string timeFormat;
        public string Time { get { return msg.Time.ToString(timeFormat); } }
        public string DisplayText => msg.Message;

        public string Status
        {
            get
            {
                if (msg.Status == LogMessageStatus.Normal)
                {
                    return "Normal";
                }
                else if (msg.Status == LogMessageStatus.Error)
                {
                    return "Error";
                }
                else if (msg.Status == LogMessageStatus.Warning)
                {
                    return "Warning";
                }
                return "Unknown status";
            }
        }
    }
}
