using JMI.General.Identifiers;
using JMI.General.VM.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace JMI.General.VM.IdentifiersSelection
{
    public abstract class IdentitySelectionListViewModel<T, TViewModel> : CloseDown
        where T : IIdentityCollectionItem
        where TViewModel : IIdentitySelectionListItemViewModel<T>
    {
        #region constructors
        public IdentitySelectionListViewModel(IReadOnlyIdentityCollection<T> identityCollection)
        {
            collection = identityCollection ?? throw new ArgumentNullException(nameof(identityCollection) + " can not be null");
            collection.CollectionChangeAdded += OnCollectionChangeAdded;
            collection.CollectionChangeRemoved += OnCollectionChangeRemoved;
            collection.CollectionChangeCleared += OnCollectionChangeCleared;

            viewModelDictionary = new Dictionary<IIdentifier, Tuple<T, TViewModel>>();
            allItems = new ObservableCollection<IIdentitySelectionListItemViewModel<T>>();
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

            commandGroupsList = CreateCommandGroups();
            CommandGroups = new ReadOnlyCollection<CommandGroupViewModel>(commandGroupsList);

            CreateViewModels();
        }
        #endregion

        #region properties
        protected IReadOnlyIdentityCollection<T> collection;
        protected Dictionary<IIdentifier, Tuple<T, TViewModel>> viewModelDictionary;

        protected ObservableCollection<IIdentitySelectionListItemViewModel<T>> allItems;

        public ListCollectionView AllItems { get; protected set; }
        public ListCollectionView CheckedItems { get; protected set; }
        public ListCollectionView SelectedItems { get; protected set; }

        /// <summary>
        /// Collection for the list commands.
        /// </summary>
        protected IList<CommandGroupViewModel> commandGroupsList;
        /// <summary>
        /// Collection of command groups
        /// </summary>
        public ReadOnlyCollection<CommandGroupViewModel> CommandGroups { get; private set; }
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
                            param => CheckAll(),
                            param => AllItems.Count > 0);
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
                            param => UnCheckAll(),
                            param => CheckedItems.Count > 0);
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
                            param => InvertChecked(),
                            param => AllItems.Count > 0);
                    invertCheckedCommand = new CommandViewModel("Invert checked", invertCheckedRelay);
                }
                return invertCheckedCommand;
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
                            param => CheckSelected(),
                            param => SelectedItems.Count > 0);
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
                            param => UnCheckSelected(),
                            param => SelectedItems.Count > 0);
                    unCheckSelectedCommand = new CommandViewModel("Uncheck selected", unCheckSelectedRelay);
                }
                return unCheckSelectedCommand;
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
        protected abstract TViewModel CreateViewModel(T item);

        public override void Close()
        {
            collection.CollectionChangeAdded -= OnCollectionChangeAdded;
            collection.CollectionChangeRemoved -= OnCollectionChangeRemoved;
            collection.CollectionChangeCleared -= OnCollectionChangeCleared;
            collection = null;

            allItems.Clear();
            allItems = null;
            viewModelDictionary.Clear();
            viewModelDictionary = null;
            
            base.Close();
        }

        private bool IsCheckedFilter(object obj)
        {
            return ((IIdentitySelectionListItemViewModel<T>)obj).IsChecked;
        }

        private bool IsSelectedFilter(object obj)
        {
            return ((IIdentitySelectionListItemViewModel<T>)obj).IsSelected;
        }

        public void CheckAll()
        {
            foreach (IIdentitySelectionListItemViewModel<T> item in allItems)
            {
                item.IsChecked = true;
            }
        }

        public void UnCheckAll()
        {
            foreach (IIdentitySelectionListItemViewModel<T> item in allItems)
            {
                item.IsChecked = false;
            }
        }

        public void InvertChecked()
        {
            foreach (IIdentitySelectionListItemViewModel<T> item in allItems)
            {
                item.IsChecked = !item.IsChecked;
            }
        }

        public void CheckSelected()
        {
            foreach (IIdentitySelectionListItemViewModel<T> item in SelectedItems)
            {
                item.IsChecked = true;
            }
        }

        public void UnCheckSelected()
        {
            foreach (IIdentitySelectionListItemViewModel<T> item in SelectedItems)
            {
                item.IsChecked = false;
            }
        }

        public IEnumerable<T> GetAllTargetItems()
        {
            List<T> list = new List<T>();
            foreach (IIdentitySelectionListItemViewModel<T> selectionItem in AllItems)
            {
                list.Add(selectionItem.Target);
            }
            return list;
        }

        public IEnumerable<T> GetCheckedTargetItems()
        {
            List<T> list = new List<T>();
            foreach (IIdentitySelectionListItemViewModel<T> selectionItem in CheckedItems)
            {
                list.Add(selectionItem.Target);
            }
            return list;
        }

        public IEnumerable<T> GetSelectedTargetItems()
        {
            List<T> list = new List<T>();
            foreach (IIdentitySelectionListItemViewModel<T> selectionItem in SelectedItems)
            {
                list.Add(selectionItem.Target);
            }
            return list;
        }

        /// <summary>
        /// Creates following commands:
        /// <para/>- Check all,
        /// <para/>- Uncheck all,
        /// <para/>- Invert Checked,
        /// <para/>- Check selected,
        /// <para/>- Uncheck selected and
        /// </summary>
        /// <returns></returns>
        private IList<CommandViewModel> CreateCommands()
        {
            List<CommandViewModel> list = new List<CommandViewModel>()
            {
                CheckAllCommand,
                UnCheckAllCommand,
                InvertCheckedCommand,
                CheckSelectedCommand,
                UnCheckSelectedCommand
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

        private void CreateViewModels()
        {
            foreach (T item in collection)
            {
                TViewModel vm = CreateViewModel(item);
                viewModelDictionary.Add(vm.Target.Identifier, new Tuple<T, TViewModel>(vm.Target, vm));
                allItems.Add(vm);
            }
        }
        #endregion

        #region events
        #endregion

        #region event handlers
        private void OnCollectionChangeCleared(object sender, EventArgs e)
        {
            viewModelDictionary.Clear();
            allItems.Clear();
        }

        private void OnCollectionChangeRemoved(object sender, IdentityCollectionRemoveEventArgs e)
        {
            foreach (IIdentifier item in e.RemovedItems)
            {
                TViewModel vm = viewModelDictionary[item].Item2;
                allItems.Remove(vm);
                viewModelDictionary.Remove(item);
            }
        }

        private void OnCollectionChangeAdded(object sender, IdentityCollectionAddEventArgs<T> e)
        {
            foreach (T item in e.AddedItems)
            {
                TViewModel vm = CreateViewModel(item);
                viewModelDictionary.Add(item.Identifier, new Tuple<T, TViewModel>(item, vm));
                allItems.Add(vm);
            }
        }
        #endregion
    }
}
