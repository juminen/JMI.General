using System;
using System.Collections.Generic;

namespace JMI.General.Identifiers
{
    public interface IReadOnlyIdentityCollection<T>: ICloseDown, IEnumerable<T> where T : IIdentityCollectionItem
    {
        /// <summary>
        /// Gets the number of items in collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Event is sent when new items are added to collection>.
        /// </summary>
        event EventHandler<IdentityCollectionAddEventArgs<T>> CollectionChangeAdded;
        /// <summary>
        /// Event is sent when items are removed from collection>.
        /// </summary>
        event EventHandler<IdentityCollectionRemoveEventArgs> CollectionChangeRemoved;
        /// <summary>
        /// Event is sent when all items in collection are removed using method <see cref="Clear"/>.
        /// </summary>
        event EventHandler CollectionChangeCleared;

        /// <summary>
        /// Checks if item is in collection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>True if item is in collection.</returns>
        bool Contains(T item);
    }
}