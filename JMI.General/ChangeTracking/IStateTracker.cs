using System;

namespace JMI.General.ChangeTracking
{
    public interface IStateTracker
    {
        bool SameState { get; }
        void Dispose();
        event EventHandler StateChanged;
    }
}