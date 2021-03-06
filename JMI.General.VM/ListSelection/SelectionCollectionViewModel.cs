﻿using JMI.General.ListSelection;
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
    /// <summary>
    /// Abstract base class for selection collection viewmodels
    /// </summary>
    /// <typeparam name="T">Type of the model list item</typeparam>
    /// <typeparam name="TViewModel">Type of the viewmodel list item</typeparam>
    [Obsolete("Use namespace Selections")]
    public abstract class SelectionCollectionViewModel<T, TViewModel> : ObservableObject
        where T : ISelectionCollectionItem
        where TViewModel : ISelectionCollectionItemViewModel
    {
        #region constructors
        /// <summary>
        /// Default constuctor
        /// </summary>
        /// <param name="selectionCollection">Model selection collection containing list items</param>
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
        /// <summary>
        /// Collection for selection collection items
        /// </summary>
        protected readonly ISelectionCollection<T> collection;
        /// <summary>
        /// Collection for selection collection viewmodel items
        /// </summary>
        protected ObservableCollection<TViewModel> allItems;
        /// <summary>
        /// Collection for the list commands.
        /// </summary>
        protected IList<CommandGroupViewModel> commandGroupsList;
        /// <summary>
        /// Collection of command groups
        /// </summary>
        public ReadOnlyCollection<CommandGroupViewModel> CommandGroups { get; private set; }
        /// <summary>
        ///  Collectionview for all selection collection list items
        /// </summary>
        public ListCollectionView AllItems { get; protected set; }

        private bool showIdColumn;
        /// <summary>
        /// For toggling Id column visibility
        /// </summary>
        public bool ShowIdColumn
        {
            get { return showIdColumn; }
            set { SetProperty(ref showIdColumn, value); }
        }

        private string displayTextColumnHeader;
        /// <summary>
        /// Text to display in column header
        /// </summary>
        public string DisplayTextColumnHeader
        {
            get { return displayTextColumnHeader; }
            set { SetProperty(ref displayTextColumnHeader, value); }
        }
        #endregion

        #region commands
        private CommandViewModel checkAllCommand;
        public CommandViewModel CheckAllCommand
        {
            get
            {
                if (checkAllCommand == null)
                {
                    RelayCommand checkAllRelay =
                        new RelayCommand(
                            param => collection.CheckAll(),
                            param => collection.AllItems.Count > 0);
                    checkAllCommand = new CommandViewModel("Check all", checkAllRelay);
                }
                return checkAllCommand;
            }
        }

        private CommandViewModel unCheckAllCommand;
        public CommandViewModel UnCheckAllCommand
        {
            get
            {
                if (unCheckAllCommand == null)
                {
                    RelayCommand unCheckAllRelay =
                        new RelayCommand(
                            param => collection.UnCheckAll(),
                            param => collection.CheckedItems.Count > 0);
                    unCheckAllCommand = new CommandViewModel("Uncheck all", unCheckAllRelay);
                }
                return unCheckAllCommand;
            }
        }

        private CommandViewModel invertCheckedCommand;
        public CommandViewModel InvertCheckedCommand
        {
            get
            {
                if (invertCheckedCommand == null)
                {
                    RelayCommand invertCheckedRelay =
                        new RelayCommand(
                            param => collection.InvertChecked(),
                            param => collection.AllItems.Count > 0);
                    invertCheckedCommand = new CommandViewModel("Invert checked", invertCheckedRelay);
                }
                return invertCheckedCommand;
            }
        }

        private CommandViewModel removeCheckedCommand;
        public CommandViewModel RemoveCheckedCommand
        {
            get
            {
                if (removeCheckedCommand == null)
                {
                    RelayCommand removeCheckedRelay =
                        new RelayCommand(
                            param => collection.RemoveChecked(),
                            param => collection.CheckedItems.Count > 0);
                    removeCheckedCommand = new CommandViewModel("Remove checked", removeCheckedRelay);
                }
                return removeCheckedCommand;
            }
        }

        private CommandViewModel checkSelectedCommand;
        public CommandViewModel CheckSelectedCommand
        {
            get
            {
                if (checkSelectedCommand == null)
                {
                    RelayCommand checkSelectedRelay =
                        new RelayCommand(
                            param => collection.CheckSelected(),
                            param => collection.SelectedItems.Count > 0);
                    checkSelectedCommand = new CommandViewModel("Check selected", checkSelectedRelay);
                }
                return checkSelectedCommand;
            }
        }

        private CommandViewModel unCheckSelectedCommand;
        public CommandViewModel UnCheckSelectedCommand
        {
            get
            {
                if (unCheckSelectedCommand == null)
                {
                    RelayCommand unCheckSelectedRelay =
                        new RelayCommand(
                            param => collection.UnCheckSelected(),
                            param => collection.SelectedItems.Count > 0);
                    unCheckSelectedCommand = new CommandViewModel("Uncheck selected", unCheckSelectedRelay);
                }
                return unCheckSelectedCommand;
            }
        }

        private CommandViewModel clearListCommand;
        public CommandViewModel ClearListCommand
        {
            get
            {
                if (clearListCommand == null)
                {
                    RelayCommand clearListRelay =
                        new RelayCommand(
                            param => collection.RemoveAll(),
                            param => collection.AllItems.Count > 0);
                    clearListCommand = new CommandViewModel("Clear list", clearListRelay);
                }
                return clearListCommand;
            }
        }

        private CommandGroupViewModel selectionGroup;
        public CommandGroupViewModel SelectionGroup
        {
            get
            {
                if (selectionGroup == null)
                {
                    selectionGroup = new CommandGroupViewModel("Selection");
                    foreach (CommandViewModel item in CreateCommands())
                    {
                        selectionGroup.Commands.Add(item);
                    }
                }
                return selectionGroup;
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Abstract method for creating list item viewmodel
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract TViewModel CreateViewModel(T item);

        /// <summary>
        /// Creates following commands:
        /// <para/>- Check all,
        /// <para/>- Uncheck all,
        /// <para/>- Invert Checked,
        /// <para/>- Remove checked,
        /// <para/>- Check selected,
        /// <para/>- Uncheck selected and
        /// <para/>- Clear list
        /// </summary>
        /// <returns></returns>
        private IList<CommandViewModel> CreateCommands()
        {
            List<CommandViewModel> list = new List<CommandViewModel>()
            {
                CheckAllCommand,
                UnCheckAllCommand,
                InvertCheckedCommand,
                RemoveCheckedCommand,
                CheckSelectedCommand,
                UnCheckSelectedCommand,
                ClearListCommand
            };
            return list;
        }

        private IList<CommandGroupViewModel> CreateCommandGroups()
        {
            List<CommandGroupViewModel> list = new List<CommandGroupViewModel>
            {
                SelectionGroup
            };
            return list;
        }

        public void RemoveCommand(CommandViewModel command)
        {
            foreach (CommandGroupViewModel group in commandGroupsList)
            {
                if (group.Commands.Contains(command))
                {
                    group.Commands.Remove(command);
                }
            }
        }

        public void AddCommandGroup(CommandGroupViewModel group)
        {
            if (!commandGroupsList.Contains(group))
            {
                commandGroupsList.Add(group);
            }
        }

        public void RemoveCommandGroup(CommandGroupViewModel group)
        {
            if (commandGroupsList.Contains(group))
            {
                commandGroupsList.Remove(group);
            }
        }

        /// <summary>
        /// Clears sorting for <see cref="AllItems"/>.
        /// </summary>
        public void ClearSorting()
        {
            AllItems.SortDescriptions.Clear();
        }

        /// <summary>
        /// Default sorting for <see cref="AllItems"/>. 
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
