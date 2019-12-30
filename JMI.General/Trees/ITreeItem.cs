using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace JMI.General.Trees
{
    public interface ITreeItem
    {
        ListCollectionView Children { get; }
        string Description { get; }
        string DisplayName { get; }
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        int Level { get; }
        ITreeItem Parent { get; set; }
        string Path { get; }

        event EventHandler<TreeItemChildrenAddEventArgs> ChildrenAdded;
        event EventHandler ChildrenCleared;
        event EventHandler<TreeItemChildrenRemoveEventArgs> ChildrenRemoved;
        event EventHandler Collapsed;
        event EventHandler Expanded;
        event EventHandler Selected;
        event EventHandler UnSelected;
        event EventHandler<TreeItemParentChanged> ParentChanged;
        event EventHandler PathChanged;

        void AddChild(ITreeItem child);
        void AddChildren(IEnumerable<ITreeItem> children);
        bool IsDescendantOf(ITreeItem node);
        void RemoveAllChildren();
        void RemoveAllDescendants();
        void RemoveChild(ITreeItem child);
        void RemoveChildren(IEnumerable<ITreeItem> children);
    }
}
