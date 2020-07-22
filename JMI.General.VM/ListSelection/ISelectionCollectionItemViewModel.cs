using System;

namespace JMI.General.VM.ListSelection
{
    [Obsolete("Use namespace Selections")]
    public interface ISelectionCollectionItemViewModel : IDisposable
    {
        /// <summary>
        /// Text displayed in lists
        /// </summary>
        string DisplayText { get; }
        /// <summary>
        /// Unique id for item
        /// </summary>
        string Id { get; }
        bool IsChecked { get; set; }
        bool IsSelected { get; set; }
    }
}