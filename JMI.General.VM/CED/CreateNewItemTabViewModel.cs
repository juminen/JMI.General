using JMI.General.VM.Application;
using System;
using System.Threading.Tasks;

namespace JMI.General.VM.CED
{
    /// <summary>
    /// Base class for views aimed to create new items in tab.
    /// </summary>
    public abstract class CreateNewItemTabViewModel : TabViewModel
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
            protected set { SetProperty(ref title, value); DisplayText = title; }
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
        /// Sends event <see cref="CreateCanceled"/>
        /// and calls <see cref="RequestCloseViewModel.RequestClose"/>.
        /// </summary>
        public virtual void Cancel()
        {
            CreateCanceled?.Invoke(this, EventArgs.Empty);
            RequestClose();
        }

        /// <summary>
        /// Sends event <see cref="ItemCreated"/>
        /// </summary>
        protected void SendItemCreatedEvent()
        {
            ItemCreated?.Invoke(this, EventArgs.Empty);
        }
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
