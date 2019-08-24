using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace JMI.General.Tree
{
    public abstract class TreeItem : ObservableObject, ITreeItem
    {
        #region constructors
        public TreeItem()
        {
            children = new ObservableCollection<ITreeItem>();
            Children = new ListCollectionView(children)
            {
                IsLiveSorting = true,
                CustomSort = new TreeItemComparer()
            };
        }

        public TreeItem(ITreeItem parent) : this()
        {
            Parent = parent;
        }
        #endregion

        #region properties
        public abstract string DisplayName { get; }
        public abstract string Description { get; }
        public abstract string Path { get; }

        public int Level
        {
            get
            {
                if (parent == null)
                {
                    return 0;
                }
                else
                {
                    return parent.Level + 1;
                }
            }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                SetProperty(ref isExpanded, value);
                if (isExpanded)
                {
                    Expanded?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    Collapsed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                SetProperty(ref isSelected, value);
                if (isSelected)
                {
                    Selected?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    UnSelected?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private ITreeItem parent;
        public ITreeItem Parent
        {
            get { return parent; }
            set
            {
                ITreeItem oldParent = parent;
                ITreeItem newParent = value;
                if (oldParent != newParent && oldParent != null)
                {
                    oldParent.PathChanged -= OnPathChanged;
                }
                if (newParent != null)
                {
                    newParent.PathChanged += OnPathChanged;
                }

                if (SetProperty(ref parent, value))
                {
                    TreeItemParentChanged args = new TreeItemParentChanged(oldParent, newParent);
                    ParentChanged?.Invoke(this, args);
                    SendPathUpdate();
                }
            }
        }

        protected ObservableCollection<ITreeItem> children;
        public ListCollectionView Children { get; protected set; }
        #endregion

        #region methods
        protected virtual void SendPathUpdate()
        {
            OnPropertyChanged(nameof(Path));
            PathChanged?.Invoke(this, EventArgs.Empty);
        }

        private bool Add(ITreeItem item, bool sendEvent)
        {
            if (!children.Contains(item))
            {
                children.Add(item);
                item.Parent = this;
                if (sendEvent)
                {
                    TreeItemChildrenAddEventArgs args =
                        new TreeItemChildrenAddEventArgs(new List<ITreeItem>() { item });
                    ChildrenAdded?.Invoke(this, args);
                }
                return true;
            }
            return false;
        }

        public void AddChild(ITreeItem child)
        {
            Add(child, true);
        }

        public void AddChildren(IEnumerable<ITreeItem> children)
        {
            List<ITreeItem> added = new List<ITreeItem>();
            foreach (ITreeItem child in children)
            {
                if (Add(child, false))
                {
                    added.Add(child);
                }
            }
            if (added.Count > 0)
            {
                TreeItemChildrenAddEventArgs args =
                    new TreeItemChildrenAddEventArgs(added);
                ChildrenAdded?.Invoke(this, args);
            }
        }

        public void RemoveAllChildren()
        {
            foreach (ITreeItem child in children)
            {
                child.Parent = null;
            }
            children.Clear();
            ChildrenCleared?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveAllDescendants()
        {
            foreach (ITreeItem child in children)
            {
                RemoveAllDescendants();
            }
            RemoveAllChildren();
        }

        private bool Remove(ITreeItem child, bool sendEvent)
        {
            if (children.Contains(child))
            {
                child.Parent = null;
                children.Remove(child);
                if (sendEvent)
                {
                    TreeItemChildrenRemoveEventArgs args =
                        new TreeItemChildrenRemoveEventArgs(new List<ITreeItem>() { child });
                    ChildrenRemoved?.Invoke(this, args);
                }
                return true;
            }
            return false;
        }

        public void RemoveChild(ITreeItem child)
        {
            Remove(child, true);
        }

        public void RemoveChildren(IEnumerable<ITreeItem> children)
        {
            List<ITreeItem> removed = new List<ITreeItem>();
            foreach (ITreeItem child in children)
            {
                if (Remove(child, false))
                {
                    removed.Add(child);
                }
            }
            if (removed.Count > 0)
            {
                TreeItemChildrenRemoveEventArgs args =
                    new TreeItemChildrenRemoveEventArgs(removed);
                ChildrenRemoved?.Invoke(this, args);
            }
        }

        public bool IsDescendantOf(ITreeItem node)
        {
            if (parent == null)
            {
                return false;
            }

            if (Parent == node)
            {
                return true;
            }
            return Parent.IsDescendantOf(node);
        }
        #endregion

        #region events
        public event EventHandler Expanded;
        public event EventHandler Collapsed;
        public event EventHandler Selected;
        public event EventHandler UnSelected;

        public event EventHandler<TreeItemChildrenAddEventArgs> ChildrenAdded;
        public event EventHandler<TreeItemChildrenRemoveEventArgs> ChildrenRemoved;
        public event EventHandler ChildrenCleared;
        public event EventHandler<TreeItemParentChanged> ParentChanged;
        public event EventHandler PathChanged;
        #endregion

        #region event handlers
        private void OnPathChanged(object sender, EventArgs e)
        {
            SendPathUpdate();
        }
        #endregion
    }
}
