using System;
using System.Threading.Tasks;

namespace JMI.General.VM.CED
{
    /// <summary>
    /// Base class for views aimed to delete items.
    /// </summary>
    public abstract class DeleteItemViewModel : ObservableObject, IDisposable
    {
        #region constructors
        #endregion

        #region properties
        private string title;
        /// <summary>
        /// For view title/header
        /// </summary>
        public string Title
        {
            get { return title; }
            protected set { SetProperty(ref title, value); }
        }
        #endregion

        #region commands
        private RelayCommand yesCommand;
        /// <summary>
        /// Calls method <see cref="DeleteItemAsync"/>. Command is always enabled.
        /// </summary>
        public RelayCommand YesCommand
        {
            get
            {
                if (yesCommand == null)
                {
                    yesCommand = new RelayCommand(
                        async param => await DeleteItemAsync(), 
                        param => true);
                }
                return yesCommand;
            }
        }

        private RelayCommand noCommand;
        /// <summary>
        /// Calls method <see cref="Cancel"/>. Command is always enabled.
        /// </summary>
        public RelayCommand NoCommand
        {
            get
            {
                if (noCommand == null)
                {
                    noCommand = new RelayCommand(
                        param => Cancel(), 
                        param => true);
                }
                return noCommand;
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Absract methor for derived classes.
        /// </summary>
        protected abstract Task DeleteItemAsync();

        /// <summary>
        /// Calls <see cref="Dispose"/> and sends event <see cref="DeletionCanceled"/>.
        /// </summary>
        public virtual void Cancel()
        {
            DeletionCanceled?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Sends event <see cref="ItemDeleted"/>
        /// </summary>
        protected void SendItemDeletedEvent()
        {
            ItemDeleted?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Absract methor for derived classes.
        /// </summary>
        public abstract void Dispose();
        #endregion

        #region events
        /// <summary>
        /// Event is fired when delete is canceled (method <see cref="Cancel"/>).
        /// </summary>
        public event EventHandler DeletionCanceled;
        /// <summary>
        /// Event is fired when item is deleted. Use <see cref="SendItemDeletedEvent"/> to send event.
        /// </summary>
        public event EventHandler ItemDeleted;
        #endregion
    }
}
