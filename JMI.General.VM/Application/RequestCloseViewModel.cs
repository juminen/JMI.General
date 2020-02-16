using System;
using System.Threading.Tasks;

namespace JMI.General.VM.Application
{
    /// <summary>
    /// Class that sends request for close. This is used eg. in situation where 
    /// usercontrol wants to close. This is done by sending event to 
    /// notify usercontrol parent to close the (child) usercontrol.
    /// </summary>
    public abstract class RequestCloseViewModel : ObservableObject, IRequestCloseViewModel
    {
        #region constructors
        #endregion

        #region properties
        #endregion

        #region commands
        #endregion

        #region methods
        /// <summary>
        /// Does not do anything, override method in implementing classes if neeeded.
        /// </summary>
        public virtual void Dispose()
        {
            //Do nothing
        }

        /// <summary>
        /// Calls <see cref="Dispose"/> and sends event <see cref="CloseRequested"/>
        /// </summary>
        protected void RequestClose()
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
