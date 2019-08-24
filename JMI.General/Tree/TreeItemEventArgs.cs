using System;
using System.Collections.Generic;

namespace JMI.General.Tree
{
    public class TreeItemChildrenAddEventArgs : EventArgs
    {
        public IEnumerable<ITreeItem> AddedItems { get; private set; }

        public TreeItemChildrenAddEventArgs(IEnumerable<ITreeItem> addedItems)
        {
            AddedItems = new List<ITreeItem>(addedItems);
        }
    }

    public class TreeItemChildrenRemoveEventArgs : EventArgs
    {
        /// <summary>
        /// Ids of the removed items
        /// </summary>
        public IEnumerable<ITreeItem> RemovedItems { get; private set; }

        public TreeItemChildrenRemoveEventArgs(IEnumerable<ITreeItem> removedItems)
        {
            RemovedItems = new List<ITreeItem>(removedItems);
        }
    }

    public class TreeItemParentChanged : EventArgs
    {
        public TreeItemParentChanged(ITreeItem oldParent, ITreeItem newParent)
        {
            if (oldParent != null)
            {
                OldParent = oldParent;
            }
            if (newParent != null)
            {
                NewParent = newParent;
            }

        }
        public ITreeItem NewParent { get; private set; }
        public ITreeItem OldParent { get; private set; }
    }
}
