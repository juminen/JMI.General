using JMI.General.Identifiers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace JMI.General.VM.Trees
{
    public abstract class IdentityTreeViewModel<T> : CloseDown where T : IIdentityTreeItem<T>
    {
        #region constructors
        public IdentityTreeViewModel(IIdentityTreeManager<T> identityTreeManager)
        {
            treeManager = identityTreeManager ?? throw new ArgumentNullException(nameof(identityTreeManager) + " can not be null");
            modelDictionary = new Dictionary<string, T>();
            viewModelDictionary = new Dictionary<string, IIdentityTreeItemViewModel<T>>();
            allNodes = new ObservableCollection<IIdentityTreeItemViewModel<T>>();

            AllNodes = new ListCollectionView(allNodes)
            {
                CustomSort = new TreeItemNameComparer<T>()
            };
            RootNodes = new ListCollectionView(allNodes)
            {
                Filter = new Predicate<object>(RootNodeFilter),
                IsLiveFiltering = true,
                CustomSort = new TreeItemNameComparer<T>()
            };
            RootNodes.LiveFilteringProperties.Add(nameof(IIdentityTreeItemViewModel<T>.Parent));

            CheckedNodes = new ListCollectionView(allNodes)
            {
                Filter = new Predicate<object>(CheckedNodeFilter),
                IsLiveFiltering = true
            };
            CheckedNodes.LiveFilteringProperties.Add(nameof(IIdentityTreeItemViewModel<T>.IsChecked));

            ExpandedNodes = new ListCollectionView(allNodes)
            {
                Filter = new Predicate<object>(ExpandedNodeFilter),
                IsLiveFiltering = true
            };
            ExpandedNodes.LiveFilteringProperties.Add(nameof(IIdentityTreeItemViewModel<T>.IsExpanded));

            SelectedNodes = new ListCollectionView(allNodes)
            {
                Filter = new Predicate<object>(SelectedNodeFilter),
                IsLiveFiltering = true
            };
            SelectedNodes.LiveFilteringProperties.Add(nameof(IIdentityTreeItemViewModel<T>.IsSelected));

            CreateRootViewModels();
            treeManager.AllItems.CollectionChangeAdded += OnAllItemsCollectionChangeAdded;
            treeManager.AllItems.CollectionChangeRemoved += OnAllItemsCollectionChangeRemoved;
        }
        #endregion

        #region properties
        protected IIdentityTreeManager<T> treeManager;
        /// <summary>
        /// Contains tree items from model. Key is identifier string from target/>
        /// </summary>
        protected Dictionary<string, T> modelDictionary;
        /// <summary>
        /// Contains viewmodels fro tree items. Key is identifier string from target/>
        /// </summary>
        protected Dictionary<string, IIdentityTreeItemViewModel<T>> viewModelDictionary;
        /// <summary>
        /// Observable collection for listcollectionviews.
        /// </summary>
        protected ObservableCollection<IIdentityTreeItemViewModel<T>> allNodes;

        public ListCollectionView AllNodes { get; protected set; }
        public ListCollectionView RootNodes { get; protected set; }
        public ListCollectionView CheckedNodes { get; protected set; }
        public ListCollectionView ExpandedNodes { get; protected set; }
        public ListCollectionView SelectedNodes { get; protected set; }

        private IIdentityTreeItemViewModel<T> selectedTreeItem;
        public IIdentityTreeItemViewModel<T> SelectedTreeItem
        {
            get { return selectedTreeItem; }
            private set
            {

                if (SetProperty(ref selectedTreeItem, value))
                {
                    SelectedTreeItemChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion

        #region commands
        #endregion

        #region methods
        private void CreateRootViewModels()
        {
            foreach (T treeItem in RootNodes)
            {
                CreateViewModelAndAddToCollections(treeItem);
            }
        }

        protected abstract IIdentityTreeItemViewModel<T> CreateViewModel(T item);

        private void CreateViewModelAndAddToCollections(T treeItem)
        {
            if (modelDictionary.ContainsKey(treeItem.Identifier.Id))
            {
                return;
            }
            IIdentityTreeItemViewModel<T> vm = CreateViewModel(treeItem);
            vm.Expanded += OnViewModelExpanded;
            vm.Selected += OnViewModelSelected;
            treeItem.ParentChanged += OnTreeItemParentChanged;
            modelDictionary.Add(treeItem.Identifier.Id, treeItem);
            viewModelDictionary.Add(treeItem.Identifier.Id, vm);
            allNodes.Add(vm);
            if (treeItem.Parent != null)
            {
                IIdentityTreeItemViewModel<T> parentVM = viewModelDictionary[treeItem.Parent.Identifier.Id];
                vm.Parent = parentVM;
                parentVM.AddChild(vm);
            }

            foreach (var item in treeItem.Children)
            {
                CreateViewModelAndAddToCollections(item);
            }
        }

        private void RemoveViewModelFromCollections(IIdentifier identifier)
        {
            if (!modelDictionary.ContainsKey(identifier.Id))
            {
                return;
            }

            IIdentityTreeItemViewModel<T> vm = viewModelDictionary[identifier.Id];
            vm.Expanded -= OnViewModelExpanded;
            vm.Selected -= OnViewModelSelected;
            vm.Target.ParentChanged -= OnTreeItemParentChanged;
            if (SelectedTreeItem == vm)
            {
                SelectedTreeItem = null;
            }
            modelDictionary.Remove(vm.Identifier.Id);
            viewModelDictionary.Remove(vm.Identifier.Id);
            allNodes.Remove(vm);
            if (vm.Parent != null)
            {
                IIdentityTreeItemViewModel<T> parentVM = viewModelDictionary[vm.Parent.Identifier.Id];
                parentVM.RemoveChild(identifier);
            }
            foreach (var item in vm.Target.Children)
            {
                RemoveViewModelFromCollections(item.Identifier);
            }
        }

        public override void Close()
        {
            foreach (IIdentityTreeItemViewModel<T> vm in viewModelDictionary.Values)
            {
                vm.Close();
            }
            RootNodes = null;
            CheckedNodes = null;
            ExpandedNodes = null;
            SelectedNodes = null;

            modelDictionary.Clear();
            modelDictionary = null;
            viewModelDictionary.Clear();
            viewModelDictionary = null;

            allNodes.Clear();
            allNodes = null;

            treeManager.AllItems.CollectionChangeAdded -= OnAllItemsCollectionChangeAdded;
            treeManager.AllItems.CollectionChangeRemoved -= OnAllItemsCollectionChangeRemoved;
            treeManager = null;
            base.Close();
        }

        public IEnumerable<IIdentityTreeItemViewModel<T>> GetCheckedTreeItems()
        {
            return viewModelDictionary.Values.ToList().Where(vm => vm.IsChecked == true).ToList();
        }

        public IEnumerable<IIdentityTreeItemViewModel<T>> GetVisibleTreeItems()
        {
            return viewModelDictionary.Values.ToList().Where(vm => vm.IsVisible == true).ToList();
        }

        public IEnumerable<IIdentityTreeItemViewModel<T>> GetSelectedTreeItems()
        {
            return viewModelDictionary.Values.ToList().Where(vm => vm.IsSelected == true).ToList();
        }
        #endregion

        #region events
        public event EventHandler SelectedTreeItemChanged;
        #endregion

        #region event handlers
        private void OnAllItemsCollectionChangeAdded(object sender, IdentityCollectionAddEventArgs<T> e)
        {
            foreach (var treeItem in e.AddedItems)
            {
                CreateViewModelAndAddToCollections(treeItem);
            }
        }

        private void OnAllItemsCollectionChangeRemoved(object sender, IdentityCollectionRemoveEventArgs e)
        {
            foreach (var id in e.RemovedItems)
            {
                RemoveViewModelFromCollections(id);
            }
        }

        private void OnViewModelExpanded(object sender, EventArgs e)
        {
            IIdentityTreeItemViewModel<T> vm = sender as IIdentityTreeItemViewModel<T>;
            treeManager.RefreshChildren(modelDictionary[vm.Identifier.Id]);
        }

        private void OnViewModelSelected(object sender, EventArgs e)
        {
            SelectedTreeItem = sender as IIdentityTreeItemViewModel<T>;
        }

        private void OnTreeItemParentChanged(object sender, IdentityTreeItemParentChangedEventArgs<T> e)
        {
            IIdentityTreeItem<T> treeItem = sender as IIdentityTreeItem<T>;
            IIdentityTreeItemViewModel<T> vm = viewModelDictionary[treeItem.Identifier.Id];

            if (e.OldParent != null)
            {
                IIdentityTreeItemViewModel<T> oldParentVM = viewModelDictionary[e.OldParent.Identifier.Id];
                oldParentVM.RemoveChild(vm.Identifier);
            }

            if (e.NewParent != null)
            {
                IIdentityTreeItemViewModel<T> newParentVM = viewModelDictionary[e.NewParent.Identifier.Id];
                newParentVM.AddChild(vm);
            }
        }
        #endregion

        #region filters
        protected bool RootNodeFilter(object obj)
        {
            return ((IIdentityTreeItemViewModel<T>)obj).Parent == null;
        }

        protected bool CheckedNodeFilter(object obj)
        {
            return ((IIdentityTreeItemViewModel<T>)obj).IsChecked;
        }

        protected bool ExpandedNodeFilter(object obj)
        {
            return ((IIdentityTreeItemViewModel<T>)obj).IsExpanded;
        }

        protected bool SelectedNodeFilter(object obj)
        {
            return ((IIdentityTreeItemViewModel<T>)obj).IsSelected;
        }
        #endregion
    }
}