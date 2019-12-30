using JMI.General.IO;
using JMI.General.ListSelection;
using JMI.General.Logging;
using System.Collections.Generic;
using System.IO;

namespace JMI.General.VM.IO
{
    public class FolderListViewModel : SelectionCollection<FolderListItemViewModel>
    {
        #region constructors
        #endregion

        #region properties
        private Logger logger = Logger.Instance;

        private FolderListItemViewModel currentFolder;
        public FolderListItemViewModel CurrentFolder
        {
            get { return currentFolder; }
            set { SetProperty(ref currentFolder, value); }
        }
        #endregion

        #region commands
        private RelayCommand launchFolderCommand;
        public RelayCommand LaunchFolderCommand
        {
            get
            {
                if (launchFolderCommand == null)
                {
                    launchFolderCommand = 
                        new RelayCommand(
                            param => LaunchCurrentFolder(), 
                            param => currentFolder != null);
                }
                return launchFolderCommand;
            }
        }
        #endregion

        #region methods
        public void ChangePath(string directoryPath)
        {
            RemoveAll();
            ActionResult<IEnumerable<DirectoryInfo>> actionResult = DirectoryCrawler.GetSubFolders(directoryPath, false);
            if (actionResult.Successful)
            {
                foreach (DirectoryInfo item in actionResult.Result)
                {
                    FolderListItemViewModel vm = new FolderListItemViewModel(item);
                    AddItem(vm);
                }
            }
            else
            {
                logger.Log(LogFactory.CreateWarningMessage(actionResult.GetComments()));
            }
        }

        private void LaunchCurrentFolder()
        {
            if (CurrentFolder != null)
            {
                System.Diagnostics.Process.Start(CurrentFolder.Path);
            }
        }
        #endregion

        #region events
        #endregion

        #region event handlers
        #endregion
    }
}
