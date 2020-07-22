using JMI.General.Identifiers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace JMI.General.Selections
{
    public class SelectionCollection<T> : ObservableObject, ISelectionCollection<T>
        where T : ISelectionTarget
    {
        #region constructors
        public SelectionCollection()
        {
            allItems = new ObservableCollection<ISelectionItem<T>>();
            AllItems = new ListCollectionView(allItems);

            CheckedItems = new ListCollectionView(allItems)
            {
                Filter = new Predicate<object>(IsCheckedFilter),
                IsLiveFiltering = true
            };
            CheckedItems.LiveFilteringProperties.Add("IsChecked");

            SelectedItems = new ListCollectionView(allItems)
            {
                Filter = new Predicate<object>(IsSelectedFilter),
                IsLiveFiltering = true
            };
            SelectedItems.LiveFilteringProperties.Add("IsSelected");
        }
        #endregion

        #region properties
        protected ObservableCollection<ISelectionItem<T>> allItems;

        //ListCollectionView is used because live filtering.
        //Other methods (ICollectionView and others) does not work.
        //ListCollectionView contains all required properties and methods.
        public ListCollectionView AllItems { get; protected set; }
        public ListCollectionView CheckedItems { get; protected set; }
        public ListCollectionView SelectedItems { get; protected set; }
        #endregion

        #region methods
        private bool IsCheckedFilter(object obj)
        {
            return ((ISelectionItem<T>)obj).IsChecked;
        }

        private bool IsSelectedFilter(object obj)
        {
            return ((ISelectionItem<T>)obj).IsSelected;
        }

        private void Add(IEnumerable<T> targetItems)
        {
            List<ISelectionItem<T>> added = new List<ISelectionItem<T>>();
            foreach (T item in targetItems)
            {
                if (!allItems.Any(x => x.Target.Identifier.Id.Equals(item.Identifier.Id)))
                {
                    ISelectionItem<T> selectionItem = new SelectionItem<T>(item);
                    allItems.Add(selectionItem);
                    added.Add(selectionItem);
                }
            }
            if (added.Count > 0)
            {
                SelectionCollectionAddEventArgs<T> args =
                    new SelectionCollectionAddEventArgs<T>(added);
                CollectionChangeAdded?.Invoke(this, args);
            }
        }

        public void AddItem(T targetItem)
        {
            Add(new List<T>() { targetItem });
        }

        public void AddRange(IEnumerable<T> targetItems)
        {
            Add(targetItems);
        }

        public void RemoveAll()
        {
            allItems.Clear();
            CollectionChangeCleared?.Invoke(this, EventArgs.Empty);
        }

        private bool Remove(T targetItem, bool sendEvent)
        {
            if (allItems.Any(x => x.Target.Identifier.Id.Equals(targetItem.Identifier.Id)))
            {
                allItems.Remove(allItems.First(x => x.Target.Identifier.Id.Equals(targetItem.Identifier.Id)));
                if (sendEvent)
                {
                    SelectionCollectionRemoveEventArgs args =
                        new SelectionCollectionRemoveEventArgs(new List<IIdentifier>() { targetItem.Identifier });
                    CollectionChangeRemoved?.Invoke(this, args);
                }
                return true;
            }
            return false;
        }

        public void RemoveRange(IEnumerable<T> targetItems)
        {
            List<IIdentifier> removed = new List<IIdentifier>();
            foreach (T target in targetItems)
            {
                if (Remove(target, false))
                {
                    removed.Add(target.Identifier);
                }
            }
            if (removed.Count > 0)
            {
                SelectionCollectionRemoveEventArgs args =
                    new SelectionCollectionRemoveEventArgs(removed);
                CollectionChangeRemoved?.Invoke(this, args);
            }
        }

        public void RemoveChecked()
        {
            List<T> removeList = new List<T>();
            foreach (ISelectionItem<T> selectionItem in CheckedItems)
            {
                removeList.Add(selectionItem.Target);
            }
            RemoveRange(removeList);
        }

        public void RemoveSelected()
        {
            List<T> removeList = new List<T>();
            //foreach (T item in SelectedItems)
            foreach (ISelectionItem<T> selectionItem in SelectedItems)
            {
                removeList.Add(selectionItem.Target);
            }
            RemoveRange(removeList);
        }

        public void CheckAll()
        {
            foreach (ISelectionItem<T> item in allItems)
            {
                item.IsChecked = true;
            }
        }

        public void UnCheckAll()
        {
            foreach (ISelectionItem<T> item in allItems)
            {
                item.IsChecked = false;
            }
        }

        public void InvertChecked()
        {
            foreach (ISelectionItem<T> item in allItems)
            {
                item.IsChecked = !item.IsChecked;
            }
        }

        public void CheckSelected()
        {
            foreach (ISelectionItem<T> item in SelectedItems)
            {
                item.IsChecked = true;
            }
        }

        public void UnCheckSelected()
        {
            foreach (ISelectionItem<T> item in SelectedItems)
            {
                item.IsChecked = false;
            }
        }

        public IEnumerable<T> GetAllTargetItems()
        {
            return allItems.Select(x => x.Target).ToList();
        }

        public IEnumerable<T> GetCheckedTargetItems()
        {
            List<T> list = new List<T>();
            foreach (ISelectionItem<T> selectionItem in CheckedItems)
            {
                list.Add(selectionItem.Target);
            }
            return list;
        }

        public IEnumerable<T> GetSelectedTargetItems()
        {
            List<T> list = new List<T>();
            foreach (ISelectionItem<T> selectionItem in SelectedItems)
            {
                list.Add(selectionItem.Target);
            }
            return list;
        }
        #endregion

        #region events
        public event EventHandler<SelectionCollectionAddEventArgs<T>> CollectionChangeAdded;
        public event EventHandler<SelectionCollectionRemoveEventArgs> CollectionChangeRemoved;
        public event EventHandler CollectionChangeCleared;
        #endregion

        #region event handlers
        #endregion
    }
}
