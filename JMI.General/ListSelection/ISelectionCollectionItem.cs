using System;
using System.ComponentModel;

namespace JMI.General.ListSelection
{
    [Obsolete("Use namespace Selections")]
    public interface ISelectionCollectionItem : INotifyPropertyChanged
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

        event EventHandler Checked;
        event EventHandler Selected;
        event EventHandler UnChecked;
        event EventHandler UnSelected;

        void Dispose();
    }
}