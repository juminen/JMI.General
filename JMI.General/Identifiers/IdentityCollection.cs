﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace JMI.General.Identifiers
{
    public class IdentityCollection<T> : IEnumerable<T> where T : IIdentityCollectionItem
    {
        #region constructors
        public IdentityCollection()
        {
            identityItems = new Dictionary<string, T>();
        }
        #endregion

        #region properties
        /// <summary>
        /// Dictionary containing items. Key is <see cref="IIdentifier.Id"/>.
        /// </summary>
        protected readonly Dictionary<string, T> identityItems;
        /// <summary>
        /// Gets the number of items in collection.
        /// </summary>
        public int Count { get { return identityItems.Count; } }
        #endregion

        #region methods
        public void DoSomething()
        {
            //tags.Add(new Tag())
        }

        public IEnumerator<T> GetEnumerator()
        {
            return identityItems.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(T item)
        {
            return identityItems.ContainsKey(item.Identifier.Id);
        }

        /// <summary>
        /// Adds item to <see cref="identityItems"/> if dictionary does not contain the item's 
        /// <see cref="IIdentifier.Id"/>
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>True if item was added.</returns>
        protected virtual bool Add(T item)
        {
            if (!identityItems.ContainsKey(item.Identifier.Id))
            {
                identityItems.Add(item.Identifier.Id, item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds given item to collection, if collection does not contain this item.
        /// </summary>
        /// <param name="item">Item to add to collection.</param>
        public void AddItem(T item)
        {
            List<T> list = new List<T>();
            if (Add(item))
            {
                list.Add(item);
                IdentityCollectionAddEventArgs<T> args =
                    new IdentityCollectionAddEventArgs<T>(list);
                CollectionChangeAdded?.Invoke(this, args);
            }
        }

        /// <summary>
        /// Adds given items to collection, if collection does not contain these items.
        /// </summary>
        /// <param name="items">Items to add to collection.</param>
        public void AddRange(IEnumerable<T> items)
        {
            List<T> list = new List<T>();
            foreach (T item in items)
            {
                if (Add(item))
                {
                    list.Add(item);
                }
            }
            if (list.Count > 0)
            {
                IdentityCollectionAddEventArgs<T> args =
                    new IdentityCollectionAddEventArgs<T>(list);
                CollectionChangeAdded?.Invoke(this, args);
            }
        }

        /// <summary>
        /// Removes item from <see cref="identityItems"/> if dictionary contains the item's 
        /// <see cref="IIdentifier.Id"/>
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>True if item was removed.</returns>
        protected virtual bool Remove(T item)
        {
            if (identityItems.ContainsKey(item.Identifier.Id))
            {
                identityItems.Remove(item.Identifier.Id);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes given item from collection, if collection contains this item.
        /// </summary>
        /// <param name="item">Item to remove from collection.</param>
        public void RemoveItem(T item)
        {
            List<IIdentifier> list = new List<IIdentifier>();
            if (Remove(item))
            {
                list.Add(item.Identifier);
                IdentityCollectionRemoveEventArgs args =
                    new IdentityCollectionRemoveEventArgs(list);
                CollectionChangeRemoved?.Invoke(this, args);
            }
        }

        /// <summary>
        /// Removes given items from collection, if collection contains these items.
        /// </summary>
        /// <param name="items">Items to remove from collection.</param>
        public void RemoveRange(IEnumerable<T> items)
        {
            List<IIdentifier> list = new List<IIdentifier>();
            foreach (T target in items)
            {
                if (Remove(target))
                {
                    list.Add(target.Identifier);
                }
            }
            if (list.Count > 0)
            {
                IdentityCollectionRemoveEventArgs args =
                    new IdentityCollectionRemoveEventArgs(list);
                CollectionChangeRemoved?.Invoke(this, args);
            }
        }

        /// <summary>
        /// Removes all items from collection and sends event <see cref="CollectionChangeCleared"/>.
        /// </summary>
        public void Clear()
        {
            identityItems.Clear();
            CollectionChangeCleared?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region events
        /// <summary>
        /// Event is send when new items are added to collection>.
        /// </summary>
        public event EventHandler<IdentityCollectionAddEventArgs<T>> CollectionChangeAdded;
        /// <summary>
        /// Event is send when items are removed from collection>.
        /// </summary>
        public event EventHandler<IdentityCollectionRemoveEventArgs> CollectionChangeRemoved;
        /// <summary>
        /// Event is send when all items in collection is removed using method <see cref="Clear"/>.
        /// </summary>
        public event EventHandler CollectionChangeCleared;
        #endregion
    }
}
