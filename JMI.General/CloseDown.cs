using System;

namespace JMI.General
{
    public abstract class CloseDown : ObservableObject, ICloseDown
    {
        /// <summary>
        /// Default constructor, property <see cref="CloseCalled"/> is set to false.
        /// </summary>
        public CloseDown()
        {
            CloseCalled = false;
        }

        private bool closeCalled;
        /// <summary>
        /// If method <see cref="Close"/> is called, this property is set to true.
        /// </summary>
        public bool CloseCalled
        {
            get { return closeCalled; }
            private set { SetProperty(ref closeCalled, value); }
        }


        /// <summary>
        /// Sets property <see cref="CloseCalled"/> to true and sends event <see cref="CloseRequested"/>.
        /// </summary>
        public virtual void Close()
        {
            CloseCalled = true;
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Event is fired when method <see cref="Close"/> is called.
        /// </summary>
        public event EventHandler CloseRequested;
    }
}
