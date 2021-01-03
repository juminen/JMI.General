using JMI.General.Identifiers;
using JMI.General.Selections;
using System;

namespace JMI.General.VM.Selections
{
    public abstract class SelectionItemViewModel<T> : ObservableObject, ISelectionItemViewModel
        where T : IIdentityCollectionItem
    {
        #region constructors
        protected SelectionItemViewModel(ISelectionItem<T> selectionItem)
        {
            item = selectionItem;
            item.Checked += OnItemCheckStataChanged;
            item.UnChecked += OnItemCheckStataChanged;
            item.Selected += OnItemSelectedStataChanged;
            item.UnSelected += OnItemSelectedStataChanged;
        }
        #endregion

        #region properties
        protected readonly ISelectionItem<T> item;

        public string Id => item.Target.Identifier.Id;

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
            item.Checked -= OnItemCheckStataChanged;
            item.UnChecked -= OnItemCheckStataChanged;
            item.Selected -= OnItemSelectedStataChanged;
            item.UnSelected -= OnItemSelectedStataChanged;
        }
        #endregion

        #region events
        #endregion

        #region event handlers
        private void OnItemCheckStataChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(IsChecked));
        }

        private void OnItemSelectedStataChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(IsSelected));
        }
        #endregion
    }
}
