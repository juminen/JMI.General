using System;

namespace JMI.General.Logging
{
    public class ProgressLogger : Logger, IDisposable
    {
        #region constructors
        public ProgressLogger() : base()
        {
            Progress = new Progress<ILogMessage>();
            Progress.ProgressChanged += OnProgressChanged;
        }

        private void OnProgressChanged(object sender, ILogMessage e)
        {
            Log(e);
        }
        #endregion

        #region properties
        public Progress<ILogMessage> Progress { get; }
        #endregion

        #region methods
        #endregion

        #region events
        #endregion

        #region event handlers
        #endregion
        public void Dispose()
        {
            Progress.ProgressChanged += OnProgressChanged;
        }
    }
}
