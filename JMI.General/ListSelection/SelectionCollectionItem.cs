using System;

namespace JMI.General.ListSelection
{
    public abstract class SelectionCollectionItem : ObservableObject, IDisposable, ISelectionCollectionItem
    {
        #region constructors
        public SelectionCollectionItem()
        {
        }
        #endregion

        #region properties
        /// <summary>
        /// Unique id for item
        /// </summary>
        public abstract string Id { get; }
        /// <summary>
        /// Text displayed in lists
        /// </summary>
        public abstract string DisplayText { get; }

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
        public abstract void Dispose();
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
