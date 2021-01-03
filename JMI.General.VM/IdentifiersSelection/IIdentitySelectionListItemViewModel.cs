using JMI.General.Identifiers;
using System;

namespace JMI.General.VM.IdentifiersSelection
{
    /// <summary>
    /// Provides mechanism for selecting and checkin items in lists.
    /// </summary>
    /// <typeparam name="T">Type of the target item</typeparam>
    public interface IIdentitySelectionListItemViewModel<T> where T : IIdentityCollectionItem
    {
        /// <summary>
        /// Target item of the selection.
        /// </summary>
        T Target { get; }
        bool IsSelected { get; set; }
        bool IsChecked { get; set; }

        event EventHandler Selected;
        event EventHandler UnSelected;
        event EventHandler Checked;
        event EventHandler UnChecked;
    }
}