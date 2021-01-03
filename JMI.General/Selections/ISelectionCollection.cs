using JMI.General.Identifiers;
using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace JMI.General.Selections
{
    public interface ISelectionCollection<T>: IReadOnlySelectionCollection<T> where T : IIdentityCollectionItem
    {
        //ListCollectionView AllItems { get; }
        //ListCollectionView CheckedItems { get; }
        //ListCollectionView SelectedItems { get; }

        //event EventHandler<SelectionCollectionAddEventArgs<T>> CollectionChangeAdded;
        //event EventHandler CollectionChangeCleared;
        //event EventHandler<SelectionCollectionRemoveEventArgs> CollectionChangeRemoved;

        void AddItem(T targetItem);
        void AddRange(IEnumerable<T> targetItems);
        //void CheckAll();
        //void CheckSelected();
        //IEnumerable<T> GetAllTargetItems();
        //IEnumerable<T> GetCheckedTargetItems();
        //IEnumerable<T> GetSelectedTargetItems();
        //void InvertChecked();
        void RemoveAll();
        void RemoveChecked();
        void RemoveRange(IEnumerable<T> targetItems);
        void RemoveSelected();
        //void UnCheckAll();
        //void UnCheckSelected();
    }
}