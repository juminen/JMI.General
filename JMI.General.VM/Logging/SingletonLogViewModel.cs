using JMI.General.Logging;

namespace JMI.General.VM.Logging
{
    public class SingletonLogViewModel : LogViewModel
    {
        public SingletonLogViewModel() : base(SingletonLogger.Instance)
        {

        }
    }
}
