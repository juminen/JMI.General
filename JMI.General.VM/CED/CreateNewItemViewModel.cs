using System;
using System.Threading.Tasks;

namespace JMI.General.VM.CED
{
    /// <summary>
    /// Base class for views aimed to create new items.
    /// </summary>
    public abstract class CreateNewItemViewModel : ObservableObject, IDisposable
    {
        #region constructors
        #endregion

        #region properties
        /// <summary>
        /// Enables/disables <see cref="CreateCommand"/>
        /// </summary>
        protected abstract bool CreateEnabled { get; }

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
        private RelayCommand createCommand;
        /// <summary>
        /// Calls method <see cref="CreateItemAsync"/>. Command is enabled disabled by boolean <see cref="CreateEnabled"/>.
        /// </summary>
        public RelayCommand CreateCommand
        {
            get
            {
                if (createCommand == null)
                {
                    createCommand = new RelayCommand(
                        async param => await CreateItemAsync(),
                        param => CreateEnabled);
                }
                return createCommand;
            }
        }

        private RelayCommand cancelCommand;
        /// <summary>
        /// Calls method <see cref="Cancel"/>. Command is always enabled.
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(
                        param => Cancel(),
                        param => true);
                }
                return cancelCommand;
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Absract methor for derived classes.
        /// </summary>
        protected abstract Task CreateItemAsync();

        /// <summary>
        /// Calls <see cref="Dispose"/> and sends event <see cref="CreateCanceled"/>.
        /// </summary>
        public virtual void Cancel()
        {
            Dispose();
            CreateCanceled?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Sends event <see cref="ItemCreated"/>
        /// </summary>
        protected void SendItemCreatedEvent()
        {
            ItemCreated?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Absract methor for derived classes.
        /// </summary>
        public abstract void Dispose();
        #endregion

        #region events
        /// <summary>
        /// Event is fired when creation is canceled (method <see cref="Cancel"/>).
        /// </summary>
        public event EventHandler CreateCanceled;
        /// <summary>
        /// Event is fired when item is created succesfully. Use <see cref="SendItemCreatedEvent"/> to send event.
        /// </summary>
        public event EventHandler ItemCreated;
        #endregion

        #region event handlers
        #endregion
    }
}
