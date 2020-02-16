namespace JMI.General.VM.Application
{
    /// <summary>
    /// Class for user interface items that are closable (eg. tab, editing form etc)
    /// </summary>
    public abstract class CloseViewModel : RequestCloseViewModel, ICloseViewModel
    {
        #region constructors
        #endregion

        #region properties
        private bool allowClose;
        /// <summary>
        /// Allows closing item.
        /// </summary>
        public bool AllowClose
        {
            get { return allowClose; }
            protected set { SetProperty(ref allowClose, value); }
        }

        private string closeFailReason;
        /// <summary>
        /// Message for user if viewmodel does not allow closing.
        /// </summary>
        public string CloseFailReason
        {
            get { return closeFailReason; }
            protected set { SetProperty(ref closeFailReason, value); }
        }
        #endregion

        #region commands
        private RelayCommand closeCommand;
        /// <summary>
        /// Command is enabled by <see cref="AllowClose"/> and
        /// calls method <see cref="CloseRequested"/>
        /// </summary>
        public RelayCommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand =
                      new RelayCommand(
                          param => RequestClose(),
                          param => AllowClose);
                }
                return closeCommand;
            }
        }
        #endregion

        #region methods
        #endregion

        #region events
        #endregion

        #region event handlers
        #endregion

    }
}
