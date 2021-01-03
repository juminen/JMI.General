using JMI.General.Identifiers;
using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace JMI.General.Selections
{
    public interface IReadOnlySelectionCollection<T> where T : IIdentityCollectionItem
    {
        ListCollectionView AllItems { get; }
        ListCollectionView CheckedItems { get; }
        ListCollectionView SelectedItems { get; }

        event EventHandler CollectionChangeCleared;
        event EventHandler<SelectionCollectionAddEventArgs<T>> CollectionChangeAdded;
        event EventHandler<SelectionCollectionRemoveEventArgs> CollectionChangeRemoved;

        void CheckAll();
        void CheckSelected();
        void InvertChecked();
        void UnCheckAll();
        void UnCheckSelected();

        IEnumerable<T> GetAllTargetItems();
        IEnumerable<T> GetCheckedTargetItems();
        IEnumerable<T> GetSelectedTargetItems();
    }
}