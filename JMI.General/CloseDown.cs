using System;

namespace JMI.General
{
    public abstract class CloseDown : ICloseDown
    {
        /// <summary>
        /// Sends event <see cref="CloseRequested"/>.
        /// </summary>
        public virtual void Close()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Event is fired when method <see cref="Close"/> is called.
        /// </summary>
        public event EventHandler CloseRequested;
    }
}
