using System;

namespace JMI.General.VM.Selections
{
    public interface ISelectionItemViewModel: IDisposable
    {
        string Id { get; }
        bool IsChecked { get; set; }
        bool IsSelected { get; set; }
    }
}