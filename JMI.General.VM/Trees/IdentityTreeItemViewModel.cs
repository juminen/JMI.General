using JMI.General.Identifiers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace JMI.General.VM.Trees
{
    public abstract class IdentityTreeItemViewModel<T> : CloseDown, IIdentityTreeItemViewModel<T> where T : IIdentityTreeItem<T>
    {
        #region constructors
        public IdentityTreeItemViewModel(T target)
        {
            Target = target;
            Target.PropertyChanged += OnTargetPropertyChanged;
            chidViewModels = new Dictionary<string, IIdentityTreeItemViewModel<T>>();
            children = new ObservableCollection<IIdentityTreeItemViewModel<T>>();
            Children = new ListCollectionView(children)
            {
                IsLiveSorting = true,
                CustomSort = new TreeItemNameComparer<T>()
            };
        }
        #endregion

        #region properties        
        /// <summary>
        /// Returns identifier of the <see cref="Target"/>.
        /// </summary>
        public IIdentifier Identifier => Target.Identifier;
        /// <summary>
        ///Item that is target of the tree item.
        /// </summary>
        public T Target { get; protected set; }
        /// <summary>
        /// Returns nane of the <see cref="Target"/>.
        /// </summary>
        public string Name => Target.Name;
        /// <summary>
        /// Returns Description of the <see cref="Target"/>.
        /// </summary>
        public string Description => Target.Description;
        /// <summary>
        /// Returns full path of the <see cref="Target"/>.
        /// </summary>
        public string FullPath => Target.FullPath;

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                SetProperty(ref isChecked, value);
                if (isChecked)
                {
                    Checked?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    UnChecked?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (SetProperty(ref isExpanded, value))
                {
                    if (isExpanded)
                    {
                        Expanded?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        foreach (var item in chidViewModels.Values)
                        {
                            if (item.Children.Count == 0)
                            {
                                item.IsExpanded = false;
                            }
                        }
                        Collapsed?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (SetProperty(ref isSelected, value))
                {
                    if (isSelected)
                    {
                        Selected?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        UnSelected?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool IsVisible
        {
            get
            {
                if (Target.Parent == null ||
                    IsExpanded ||
                    Parent != null && Parent.IsExpanded)
                {
                    return true;
                }
                return false;
            }
        }

        private IIdentityTreeItemViewModel<T> parent;
        public IIdentityTreeItemViewModel<T> Parent
        {
            get { return parent; }
            set { SetProperty(ref parent, value); }
        }

        /// <summary>
        /// Key is identifier string from target/>
        /// </summary>
        protected Dictionary<string, IIdentityTreeItemViewModel<T>> chidViewModels;
        protected ObservableCollection<IIdentityTreeItemViewModel<T>> children;
        public ListCollectionView Children { get; protected set; }
        #endregion

        #region methods
        public override void Close()
        {
            Target.PropertyChanged -= OnTargetPropertyChanged;
            foreach (var item in chidViewModels.Values)
            {
                item.Close();
            }

            chidViewModels.Clear();
            chidViewModels = null;
            children.Clear();
            children = null;

            base.Close();
        }

        public void AddChild(IIdentityTreeItemViewModel<T> child)
        {
            if (!chidViewModels.ContainsKey(child.Identifier.Id))
            {
                child.Parent = this;
                chidViewModels.Add(child.Identifier.Id, child);
                children.Add(child);
            }
        }

        public void RemoveChild(IIdentifier childIdentifier)
        {
            if (chidViewModels.ContainsKey(childIdentifier.Id))
            {
                IIdentityTreeItemViewModel<T> child = chidViewModels[childIdentifier.Id];
                child.Parent = null;
                chidViewModels.Remove(childIdentifier.Id);
                children.Remove(child);
            }
        }
        #endregion

        #region events
        public event EventHandler Checked;
        public event EventHandler UnChecked;
        public event EventHandler Expanded;
        public event EventHandler Collapsed;
        public event EventHandler Selected;
        public event EventHandler UnSelected;
        #endregion

        #region event handlers        
        private void OnTargetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(IIdentityTreeItem<T>.Name)))
            {
                OnPropertyChanged(nameof(Name));
            }
            else if (e.PropertyName.Equals(nameof(IIdentityTreeItem<T>.Description)))
            {
                OnPropertyChanged(nameof(Description));
            }
            else if (e.PropertyName.Equals(nameof(IIdentityTreeItem<T>.FullPath)))
            {
                OnPropertyChanged(nameof(FullPath));
            }
        }
        #endregion
    }
}
