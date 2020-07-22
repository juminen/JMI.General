using JMI.General.ListSelection;
using System;
using System.Collections.Generic;

namespace JMI.General.VM.ListSelection
{
    [Obsolete("Use namespace Selections")]
    public abstract class SelectionCollectionItemViewModel : ObservableObject, ISelectionCollectionItemViewModel
    {
        #region constructors
        public SelectionCollectionItemViewModel(ISelectionCollectionItem selectionItem)
        {
            item = selectionItem ?? throw new ArgumentNullException(nameof(selectionItem) + " can not be null");
            item.PropertyChanged += OnItemPropertyChanged;
            FollowedProperties = new Dictionary<string, string>();
            FollowedProperties.Add(nameof(item.DisplayText), nameof(DisplayText));
            FollowedProperties.Add(nameof(item.IsChecked), nameof(IsChecked));
            FollowedProperties.Add(nameof(item.IsSelected), nameof(IsSelected));
        }
        #endregion

        #region properties
        protected readonly ISelectionCollectionItem item;
        /// <summary>
        /// Key is model item property name followed, value is the property name needed to notify.
        /// </summary>
        protected Dictionary<string, string> FollowedProperties { get; private set; }

        public string DisplayText => item.DisplayText;
        public string Id => item.Id;

        public bool IsChecked
        {
            get => item.IsChecked;
            set { item.IsChecked = value; }
        }
        public bool IsSelected
        {
            get => item.IsSelected;
            set { item.IsSelected = value; }
        }
        #endregion

        #region commands
        #endregion

        #region methods
        public virtual void Dispose()
        {
            item.PropertyChanged -= OnItemPropertyChanged;
        }
        #endregion

        #region events
        #endregion

        #region event handlers
        private void OnItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (FollowedProperties.ContainsKey(e.PropertyName))
            {
                OnPropertyChanged(FollowedProperties[e.PropertyName]);
            }
        }
        #endregion
    }
}
