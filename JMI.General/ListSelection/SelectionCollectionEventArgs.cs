using System;
using System.Collections.Generic;

namespace JMI.General.ListSelection
{
    [Obsolete("Use namespace Selections")]
    public class SelectionCollectionAddEventArgs<T> : EventArgs where T : ISelectionCollectionItem
    {
        public IEnumerable<T> AddedItems { get; private set; }

        public SelectionCollectionAddEventArgs(IEnumerable<T> addedItems)
        {
            AddedItems = new List<T>(addedItems);
        }
    }
    [Obsolete("Use namespace Selections")]
    public class SelectionCollectionRemoveEventArgs : EventArgs
    {
        /// <summary>
        /// Ids of the removed items
        /// </summary>
        public IEnumerable<string> RemovedItems { get; private set; }

        public SelectionCollectionRemoveEventArgs(IEnumerable<string> removedItems)
        {
            RemovedItems = new List<string>(removedItems);
        }
    }
}
