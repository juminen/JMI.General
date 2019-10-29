using JMI.General.ListSelection;
using JMI.General.Sorting;
using JMI.General.VM.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace JMI.General.VM.ListSelection
{
    public abstract class SelectionCollectionViewModel<T, TViewModel> : ObservableObject
        where T : ISelectionCollectionItem
        where TViewModel : ISelectionCollectionItemViewModel
    {
        #region constructors
        public SelectionCollectionViewModel(ISelectionCollection<T> selectionCollection)
        {
            collection = selectionCollection ?? throw new ArgumentNullException(nameof(selectionCollection) + " can not be null");
            collection.CollectionChangeAdded += OnCollectionChangeAdded;
            collection.CollectionChangeRemoved += OnCollectionChangeRemoved;
            collection.CollectionChangeCleared += OnCollectionChangeCleared;
            allItems = new ObservableCollection<TViewModel>();
            AllItems = new ListCollectionView(allItems);
            commandGroupsList = CreateCommandGroups();
            CommandGroups = new ReadOnlyCollection<CommandGroupViewModel>(commandGroupsList);

            ShowIdColumn = false;
            DisplayTextColumnHeader = "Item";
        }
        #endregion

        #region properties
        protected readonly ISelectionCollection<T> collection;
        protected ObservableCollection<TViewModel> allItems;
        protected IList<CommandGroupViewModel> commandGroupsList;
        public ListCollectionView AllItems { get; protected set; }

        private bool showIdColumn;
        public bool ShowIdColumn
        {
            get { return showIdColumn; }
            set { SetProperty(ref showIdColumn, value); }
        }

        private string displayTextColumnHeader;
        public string DisplayTextColumnHeader
        {
            get { return displayTextColumnHeader; }
            set { SetProperty(ref displayTextColumnHeader, value); }
        }
        #endregion

        #region commands
        public ReadOnlyCollection<CommandGroupViewModel> CommandGroups { get; private set; }

        /// <summary>
        /// Creates following commands:</br>
        /// - Check all,</br>
        /// - Uncheck all,</br>
        /// - Invert Checked,</br>
        /// - Remove checked,</br> 
        /// - Check selected,</br>
        /// - Uncheck selected and</br>
        /// - Clear list
        /// </summary>
        /// <returns></returns>
        private IList<CommandViewModel> CreateCommands()
        {
            List<CommandViewModel> list = new List<CommandViewModel>();

            RelayCommand checkAllRelay =
                new RelayCommand(
                    param => collection.CheckAll(),
                    param => collection.AllItems.Count > 0);
            CommandViewModel checkAllCvm =
                new CommandViewModel("Check all", checkAllRelay);
            list.Add(checkAllCvm);

            RelayCommand unCheckAllRelay =
                new RelayCommand(
                    param => collection.UnCheckAll(),
                    param => collection.CheckedItems.Count > 0);
            CommandViewModel unCheckAllCvm =
                new CommandViewModel("Uncheck all", unCheckAllRelay);
            list.Add(unCheckAllCvm);

            RelayCommand invertCheckedRelay =
                new RelayCommand(
                    param => collection.InvertChecked(),
                    param => collection.AllItems.Count > 0);
            CommandViewModel invertCheckedCvm =
                new CommandViewModel("Invert checked", invertCheckedRelay);
            list.Add(invertCheckedCvm);

            RelayCommand removeCheckedRelay =
                new RelayCommand(
                    param => collection.RemoveChecked(),
                    param => collection.CheckedItems.Count > 0);
            CommandViewModel removeCheckedCvm =
                new CommandViewModel("Remove checked", removeCheckedRelay);
            list.Add(removeCheckedCvm);

            RelayCommand checkSelectedRelay =
                new RelayCommand(
                    param => collection.CheckSelected(),
                    param => collection.SelectedItems.Count > 0);
            CommandViewModel checkSelectedCvm =
                new CommandViewModel("Check selected", checkSelectedRelay);
            list.Add(checkSelectedCvm);

            RelayCommand unCheckSelectedRelay =
                new RelayCommand(
                    param => collection.UnCheckSelected(),
                    param => collection.SelectedItems.Count > 0);
            CommandViewModel unCheckSelectedCvm =
                new CommandViewModel("Uncheck selected", unCheckSelectedRelay);
            list.Add(unCheckSelectedCvm);

            RelayCommand clearListRelay =
                new RelayCommand(
                    param => collection.RemoveAll(),
                    param => collection.AllItems.Count > 0);
            CommandViewModel clearListCvm =
                new CommandViewModel("Clear list", clearListRelay);
            list.Add(clearListCvm);

            return list;
        }

        private IList<CommandGroupViewModel> CreateCommandGroups()
        {
            List<CommandGroupViewModel> list = new List<CommandGroupViewModel>();
            CommandGroupViewModel group = new CommandGroupViewModel("Selection");
            foreach (CommandViewModel item in CreateCommands())
            {
                group.Commands.Add(item);
            }
            list.Add(group);
            return list;
        }
            
        #endregion

        #region methods
        protected abstract TViewModel CreateViewModel(T item);

        /// <summary>
        /// Clears sorting for <see cref="AllItems"/>.
        /// </summary>
        public void ClearSorting()
        {
            AllItems.SortDescriptions.Clear();
        }

        /// <summary>
        /// Default sorting for <see cref="AllItems"/>.</br>
        /// Uses <see cref="AlphanumComparatorFast"/> on <see cref="ISelectionCollectionItemViewModel.DisplayText"/>.
        /// </summary>
        public virtual void SetSorting()
        {
            ClearSorting();
            AllItems.CustomSort = new DefaultSorting();
        }
        #endregion

        #region events
        #endregion

        #region event handlers
        private void OnCollectionChangeAdded(object sender, SelectionCollectionAddEventArgs<T> e)
        {
            foreach (T item in e.AddedItems)
            {
                allItems.Add(CreateViewModel(item));
            }
        }

        private void OnCollectionChangeRemoved(object sender, SelectionCollectionRemoveEventArgs e)
        {
            foreach (string itemId in e.RemovedItems)
            {
                if (allItems.Any(x => x.Id.Equals(itemId)))
                {
                    allItems.Remove(allItems.First(x => x.Id.Equals(itemId)));
                }
            }
        }

        private void OnCollectionChangeCleared(object sender, EventArgs e)
        {
            allItems.Clear();
        }
        #endregion

        private class DefaultSorting : IComparer
        {
            public int Compare(object x, object y)
            {
                if (!(x is ISelectionCollectionItemViewModel item1))
                {
                    return 0;
                }
                if (!(y is ISelectionCollectionItemViewModel item2))
                {
                    return 0;
                }
                AlphanumComparatorFast comp = new AlphanumComparatorFast();
                return comp.Compare(item1.DisplayText, item2.DisplayText);
            }
        }
    }
}
