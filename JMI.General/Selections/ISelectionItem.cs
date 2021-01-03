using JMI.General.Identifiers;
using System;

namespace JMI.General.Selections
{
    public interface ISelectionItem<T> where T : IIdentityCollectionItem
    {
        /// <summary>
        /// Target item of the selection.
        /// </summary>
        T Target { get; }
        bool IsChecked { get; set; }
        bool IsSelected { get; set; }

        event EventHandler Checked;
        event EventHandler Selected;
        event EventHandler UnChecked;
        event EventHandler UnSelected;
    }
}