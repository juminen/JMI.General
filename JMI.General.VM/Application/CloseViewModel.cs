using System;
using System.Threading.Tasks;

namespace JMI.General.VM.Application
{
    /// <summary>
    /// Class for user interface items that are closable (eg. tab, editing form etc)
    /// </summary>
    public abstract class CloseViewModel : ObservableObject, IDisposable
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
        #endregion

        #region commands
        private RelayCommand closeCommand;
        /// <summary>
        /// Command is enabled by <see cref="AllowClose"/> and calls method <see cref="CloseRequested"/>
        /// </summary>
        public RelayCommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand =
                      new RelayCommand(
                          async param => await RequestClose(),
                          param => AllowClose);
                }
                return closeCommand;
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Does not do anything, override method in derived classes if neeeded.
        /// </summary>
        public virtual void Dispose()
        {
            //Do nothing
        }

        /// <summary>
        /// Calls <see cref="Dispose"/> and sends event <see cref="CloseRequested"/>
        /// </summary>
        protected virtual async Task RequestClose()
        {
            Dispose();
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region events
        /// <summary>
        /// Event is send when close is requested, <see cref="RequestClose"/>
        /// </summary>
        public event EventHandler CloseRequested;
        #endregion

        #region event handlers
        #endregion

    }
}
