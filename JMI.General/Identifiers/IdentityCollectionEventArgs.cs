using System;
using System.Collections.Generic;

namespace JMI.General.Identifiers
{
    public class IdentityCollectionAddEventArgs<T> : EventArgs
        where T : IIdentityCollectionItem
    {
        /// <summary>
        /// Items that are added to collection.
        /// </summary>
        public IEnumerable<T> AddedItems { get; private set; }
        public IdentityCollectionAddEventArgs(IEnumerable<T> addedItems)
        {
            AddedItems = new List<T>(addedItems);
        }
    }

    public class IdentityCollectionRemoveEventArgs : EventArgs
    {
        /// <summary>
        /// Unique identifiers of the removed items.
        /// </summary>
        public IEnumerable<IIdentifier> RemovedItems { get; private set; }

        public IdentityCollectionRemoveEventArgs(IEnumerable<IIdentifier> removedItems)
        {
            RemovedItems = new List<IIdentifier>(removedItems);
        }
    }
}
