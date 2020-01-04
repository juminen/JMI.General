using System;
using System.Threading.Tasks;

namespace JMI.General.VM.CED
{
    public abstract class CreateNewItemViewModel
    {
        #region constructors
        #endregion

        #region properties
        protected abstract bool CreateEnabled { get; }
        protected abstract string Title { get; }
        #endregion

        #region commands
        private RelayCommand createCommand;
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
        protected abstract Task CreateItemAsync();

        public void Cancel()
        {
            CreateCanceled?.Invoke(this, EventArgs.Empty);
        }

        protected void SendItemCreatedEvent()
        {
            ItemCreated?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region events
        public event EventHandler CreateCanceled;
        public event EventHandler ItemCreated;
        #endregion

        #region event handlers
        #endregion
    }
}
