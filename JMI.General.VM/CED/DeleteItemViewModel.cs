using System;
using System.Threading.Tasks;

namespace JMI.General.VM.CED
{
    public abstract class DeleteItemViewModel
    {
        #region constructors
        #endregion

        #region properties
        protected abstract string Title { get; }
        #endregion

        #region commands
        private RelayCommand yesCommand;
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
        protected abstract Task DeleteItemAsync();

        public void Cancel()
        {
            DeletionCanceled?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region events
        public event EventHandler DeletionCanceled;
        public event EventHandler ItemDeleted;
        #endregion
    }
}
