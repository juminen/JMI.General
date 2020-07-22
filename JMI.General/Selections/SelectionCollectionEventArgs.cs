using JMI.General.Identifiers;
using System;
using System.Collections.Generic;

namespace JMI.General.Selections
{
    public class SelectionCollectionAddEventArgs<T> : EventArgs 
        where T : ISelectionTarget
    {
        public IEnumerable<ISelectionItem<T>> AddedItems { get; private set; }
        public SelectionCollectionAddEventArgs(IEnumerable<ISelectionItem<T>> addedItems)
        {
            AddedItems = new List<ISelectionItem<T>>(addedItems);
        }
    }

    public class SelectionCollectionRemoveEventArgs : EventArgs
    {
        /// <summary>
        /// Unique ids of the removed items
        /// </summary>
        public IEnumerable<IIdentifier> RemovedItems { get; private set; }

        public SelectionCollectionRemoveEventArgs(IEnumerable<IIdentifier> removedItems)
        {
            RemovedItems = new List<IIdentifier>(removedItems);
        }
    }
}
