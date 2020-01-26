using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace JMI.General.ListSelection
{
    public class SelectionCollection<T> : ObservableObject, ISelectionCollection<T> where T : ISelectionCollectionItem
    {
        #region constructors
        public SelectionCollection()
        {
            allItems = new ObservableCollection<T>();
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
        protected ObservableCollection<T> allItems;

        public ListCollectionView AllItems { get; protected set; }
        //ListCollectionView is used because live filtering.
        //Other methods (ICollectionView and others) does not work.
        //ListCollectionView contains all required properties and methods.
        public ListCollectionView CheckedItems { get; protected set; }
        public ListCollectionView SelectedItems { get; protected set; }
        #endregion

        #region methods
        public virtual void Dispose()
        {
            foreach (T item in allItems)
            {
                item.Dispose();
            }
        }

        private bool IsCheckedFilter(object obj)
        {
            return ((T)obj).IsChecked;
        }

        private bool IsSelectedFilter(object obj)
        {
            return ((T)obj).IsSelected;
        }

        private bool Add(T item, bool sendEvent)
        {
            if (!allItems.Any(x => x.Id.Equals(item.Id)))
            {
                allItems.Add(item);
                if (sendEvent)
                {
                    SelectionCollectionAddEventArgs<T> args =
                        new SelectionCollectionAddEventArgs<T>(new List<T>() { item });
                    CollectionChangeAdded?.Invoke(this, args);
                }
                return true;
            }
            return false;
        }

        public void AddItem(T item)
        {
            Add(item, true);
        }

        public void AddRange(IEnumerable<T> items)
        {
            List<T> added = new List<T>();
            foreach (T item in items)
            {
                if (Add(item, false))
                {
                    added.Add(item);
                }
            }
            if (added.Count > 0)
            {
                SelectionCollectionAddEventArgs<T> args =
                    new SelectionCollectionAddEventArgs<T>(added);
                CollectionChangeAdded?.Invoke(this, args);
            }
        }

        public void RemoveAll()
        {
            allItems.Clear();
            CollectionChangeCleared?.Invoke(this, EventArgs.Empty);
        }

        private bool Remove(string id, bool sendEvent)
        {
            if (allItems.Any(x => x.Id.Equals(id)))
            {
                allItems.Remove(allItems.First(x => x.Id.Equals(id)));
                if (sendEvent)
                {
                    SelectionCollectionRemoveEventArgs args = 
                        new SelectionCollectionRemoveEventArgs(new List<string>() { id });
                    CollectionChangeRemoved?.Invoke(this, args);
                }
                return true;
            }
            return false;
        }

        public void RemoveRange(IEnumerable<string> itemIds)
        {
            List<string> removed = new List<string>();
            foreach (string id in itemIds)
            {
                if (Remove(id, false))
                {
                    removed.Add(id);
                }
            }
            if (removed.Count>0)
            {
                SelectionCollectionRemoveEventArgs args =
                    new SelectionCollectionRemoveEventArgs(removed);
                CollectionChangeRemoved?.Invoke(this, args);
            }
        }

        public void RemoveChecked()
        {
            List<string> ids = new List<string>();
            foreach (T item in CheckedItems)
            {
                ids.Add(item.Id);
            }
            RemoveRange(ids);
        }

        public void RemoveSelected()
        {
            List<string> ids = new List<string>();
            foreach (T item in SelectedItems)
            {
                ids.Add(item.Id);
            }
            RemoveRange(ids);
        }

        public void CheckAll()
        {
            foreach (T item in allItems)
            {
                item.IsChecked = true;
            }
        }

        public void UnCheckAll()
        {
            foreach (T item in allItems)
            {
                item.IsChecked = false;
            }
        }

        public void InvertChecked()
        {
            foreach (T item in allItems)
            {
                item.IsChecked = !item.IsChecked;
            }
        }

        public void CheckSelected()
        {
            foreach (T item in SelectedItems)
            {
                item.IsChecked = true;
            }
        }

        public void UnCheckSelected()
        {
            foreach (T item in SelectedItems)
            {
                item.IsChecked = false;
            }
        }
        
        public IEnumerable<T> GetAllItemsAsIEnumerable()
        {
            return allItems.ToList();
        }

        public IEnumerable<T> GetCheckedItemsAsIEnumerable()
        {
            return CheckedItems.Cast<T>().ToList();
        }

        public IEnumerable<T> GetSelectedItemsAsIEnumerable()
        {
            return SelectedItems.Cast<T>().ToList();
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
