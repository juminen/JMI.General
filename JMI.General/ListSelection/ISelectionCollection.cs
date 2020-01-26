using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace JMI.General.ListSelection
{
    public interface ISelectionCollection<T> where T : ISelectionCollectionItem
    {
        ListCollectionView AllItems { get; }
        ListCollectionView CheckedItems { get; }
        ListCollectionView SelectedItems { get; }

        event EventHandler<SelectionCollectionAddEventArgs<T>> CollectionChangeAdded;
        event EventHandler CollectionChangeCleared;
        event EventHandler<SelectionCollectionRemoveEventArgs> CollectionChangeRemoved;

        void AddItem(T item);
        void AddRange(IEnumerable<T> items);
        void CheckAll();
        void CheckSelected();
        void Dispose();
        IEnumerable<T> GetAllItemsAsIEnumerable();
        IEnumerable<T> GetCheckedItemsAsIEnumerable();
        IEnumerable<T> GetSelectedItemsAsIEnumerable();
        void InvertChecked();
        void RemoveAll();
        void RemoveChecked();
        void RemoveRange(IEnumerable<string> itemIds);
        void RemoveSelected();
        void UnCheckAll();
        void UnCheckSelected();
    }
}