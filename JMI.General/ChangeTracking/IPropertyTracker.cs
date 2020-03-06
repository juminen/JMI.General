using System;

namespace JMI.General.ChangeTracking
{
    interface IPropertyTracker
    {
        bool ComparisonResult { get; }
        event EventHandler ValuesChecked;
        void Dispose();
    }
}