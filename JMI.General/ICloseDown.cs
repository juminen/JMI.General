using System;

namespace JMI.General
{
    public interface ICloseDown
    {
        /// <summary>
        /// If method <see cref="Close"/> is called, this property is set to true.
        /// </summary>
        bool CloseCalled { get; }
        /// <summary>
        /// Sets property <see cref="CloseCalled"/> to true and sends event <see cref="CloseRequested"/>.
        /// </summary>
        void Close();
        /// <summary>
        /// Event is fired when method <see cref="Close"/> is called.
        /// </summary>
        event EventHandler CloseRequested;
    }
}
