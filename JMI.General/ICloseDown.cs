using System;

namespace JMI.General
{
    public interface ICloseDown
    {
        void Close();
        event EventHandler CloseRequested;
    }
}
