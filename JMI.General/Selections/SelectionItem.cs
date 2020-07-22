using System;

namespace JMI.General.Selections
{
    class SelectionItem<T> : ObservableObject, ISelectionItem<T>
        where T : ISelectionTarget
    {
        #region constructors
        public SelectionItem(T target)
        {
            Target = target;
        }
        #endregion

        #region properties
        /// <summary>
        ///Item that is target of the selection.
        /// </summary>
        public T Target { get; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                SetProperty(ref isSelected, value);
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
        #endregion

        #region methods
        #endregion

        #region events
        public event EventHandler Selected;
        public event EventHandler UnSelected;
        public event EventHandler Checked;
        public event EventHandler UnChecked;
        #endregion

        #region event handlers
        #endregion
    }
}
