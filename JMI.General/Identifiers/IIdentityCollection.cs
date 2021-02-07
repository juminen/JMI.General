using System.Collections.Generic;

namespace JMI.General.Identifiers
{
    public interface IIdentityCollection<T> : IReadOnlyIdentityCollection<T> where T : IIdentityCollectionItem
    {
        /// <summary>
        /// Adds given item to collection, if collection does not contain this item.
        /// </summary>
        /// <param name="item">Item to add to collection.</param>
        void AddItem(T item);
        /// <summary>
        /// Adds given items to collection, if collection does not contain these items.
        /// </summary>
        /// <param name="items">Items to add to collection.</param>
        void AddRange(IEnumerable<T> items);
        /// <summary>
        /// Removes all items from collection and sends event <see cref="CollectionChangeCleared"/>.
        /// </summary>
        void Clear();
        /// <summary>
        /// Removes given item from collection, if collection contains this item.
        /// </summary>
        /// <param name="item">Item to remove from collection.</param>
        void RemoveItem(T item);
        /// <summary>
        /// Removes given items from collection, if collection contains these items.
        /// </summary>
        /// <param name="items">Items to remove from collection.</param>
        void RemoveRange(IEnumerable<T> items);
        /// <summary>
        /// Get item from collection using item identifier.
        /// </summary>
        /// <param name="identifier">Identifier of the wanted item</param>
        /// <returns>Item with given identifier. 
        /// If item with given identifier does not found from collection, <see cref="ActionResult{T}.Result"/> is null.</returns>
        ActionResult<T> GetItem(IIdentifier identifier);
    }
}