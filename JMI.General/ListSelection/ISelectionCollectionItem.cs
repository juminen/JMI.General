using System;

namespace JMI.General.ListSelection
{
    public interface ISelectionCollectionItem
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