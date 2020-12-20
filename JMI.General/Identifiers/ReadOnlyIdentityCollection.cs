using System;
using System.Collections;
using System.Collections.Generic;

namespace JMI.General.Identifiers
{
    public class ReadOnlyIdentityCollection<T> : CloseDown, IReadOnlyIdentityCollection<T> where T : IIdentityCollectionItem
    {
        #region constructors
        public ReadOnlyIdentityCollection(IdentityCollection<T> collection)
        {
            identityCollection = collection ?? throw new ArgumentNullException(nameof(collection) + " can not be null");
            identityCollection.CollectionChangeAdded += IdentityCollection_CollectionChangeAdded;
            identityCollection.CollectionChangeRemoved += IdentityCollection_CollectionChangeRemoved;
            identityCollection.CollectionChangeCleared += IdentityCollection_CollectionChangeCleared;
        }
        #endregion

        #region properties
        /// <summary>
        /// Dictionary containing items. Key is <see cref="IIdentifier.Id"/>.
        /// </summary>
        protected IdentityCollection<T> identityCollection;
        /// <summary>
        /// Gets the number of items in collection.
        /// </summary>
        public int Count { get { return identityCollection.Count; } }
        #endregion

        #region methods
        public IEnumerator<T> GetEnumerator()
        {
            return identityCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Checks if item is in collection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>True if item is in collection.</returns>
        public bool Contains(T item)
        {
            return identityCollection.Contains(item);
        }

        public override void Close()
        {
            if (identityCollection != null)
            {
                identityCollection = null;
                identityCollection.CollectionChangeAdded -= IdentityCollection_CollectionChangeAdded;
                identityCollection.CollectionChangeRemoved -= IdentityCollection_CollectionChangeRemoved;
                identityCollection.CollectionChangeCleared -= IdentityCollection_CollectionChangeCleared;
            }
            base.Close();
        }
        #endregion

        #region events
        /// <summary>
        /// Event is sent when new items are added to collection>.
        /// </summary>
        public event EventHandler<IdentityCollectionAddEventArgs<T>> CollectionChangeAdded;
        /// <summary>
        /// Event is sent when items are removed from collection>.
        /// </summary>
        public event EventHandler<IdentityCollectionRemoveEventArgs> CollectionChangeRemoved;
        /// <summary>
        /// Event is sent when all items in collection are removed using method <see cref="Clear"/>.
        /// </summary>
        public event EventHandler CollectionChangeCleared;
        #endregion

        #region event handlers
        private void IdentityCollection_CollectionChangeAdded(object sender, IdentityCollectionAddEventArgs<T> e)
        {
            List<T> list = new List<T>(e.AddedItems);
            IdentityCollectionAddEventArgs<T> args =
                new IdentityCollectionAddEventArgs<T>(list);
            CollectionChangeAdded?.Invoke(this, args);
        }

        private void IdentityCollection_CollectionChangeRemoved(object sender, IdentityCollectionRemoveEventArgs e)
        {
            List<IIdentifier> list = new List<IIdentifier>(e.RemovedItems);
            IdentityCollectionRemoveEventArgs args =
                new IdentityCollectionRemoveEventArgs(list);
            CollectionChangeRemoved?.Invoke(this, args);
        }

        private void IdentityCollection_CollectionChangeCleared(object sender, EventArgs e)
        {
            CollectionChangeCleared?.Invoke(this, EventArgs.Empty);
        }
        #endregion

    }
}
