using JMI.General.Identifiers;
using System;
using System.Windows.Data;

namespace JMI.General.VM.Trees
{
    public interface IIdentityTreeItemViewModel<T>: IIdentityCollectionItem, ICloseDown where T : IIdentityTreeItem<T>
    {
        /// <summary>
        ///Item that is target of the tree item.
        /// </summary>
        T Target { get; }
        /// <summary>
        /// Parent of the item.
        /// </summary>
        IIdentityTreeItemViewModel<T> Parent { get; set; }
        /// <summary>
        /// Collection of child viewmodels
        /// </summary>
        ListCollectionView Children { get; }
        /// <summary>
        /// Name to display in tree.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Description text to display in tree.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Item full path in tree.
        /// </summary>
        string FullPath { get; }
        bool IsChecked { get; set; }
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        bool IsVisible { get; }

        void AddChild(IIdentityTreeItemViewModel<T> child);
        void RemoveChild(IIdentifier childIdentifier);

        event EventHandler Checked;
        event EventHandler UnChecked;
        event EventHandler Collapsed;
        event EventHandler Expanded;
        event EventHandler Selected;
        event EventHandler UnSelected;
    }
}