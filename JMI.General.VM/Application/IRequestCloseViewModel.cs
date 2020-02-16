using System;

namespace JMI.General.VM.Application
{
    /// <summary>
    /// Provides mechanism for requesting closing.
    /// </summary>
    public interface IRequestCloseViewModel : IDisposable
    {
        event EventHandler CloseRequested;
    }
}