using JMI.General.IO;
using JMI.General.ListSelection;
using JMI.General.Logging;
using System.Collections.Generic;
using System.IO;

namespace JMI.General.VM.IO
{
    public class FileListViewModel : SelectionCollection<FileListItemViewModel>
    {
        #region constructors
        #endregion

        #region properties
        private Logger logger = Logger.Instance;

        private FileListItemViewModel currentFile;
        public FileListItemViewModel CurrentFile
        {
            get { return currentFile; }
            set { SetProperty(ref currentFile, value); }
        }
        #endregion

        #region commands
        private RelayCommand launchFileCommand;
        public RelayCommand LaunchFileCommand
        {
            get
            {
                if (launchFileCommand == null)
                {
                    launchFileCommand = 
                        new RelayCommand(
                            param => LaunchCurrentFile(), 
                            param => currentFile != null);
                }
                return launchFileCommand;
            }
        }
        #endregion

        #region methods
        public void ChangePath(string directoryPath)
        {
            RemoveAll();
            ActionResult<IEnumerable<FileInfo>> actionResult = DirectoryCrawler.GetFiles(directoryPath, false);
            if (actionResult.Successful)
            {
                foreach (FileInfo item in actionResult.Result)
                {
                    FileListItemViewModel vm = new FileListItemViewModel(item);
                    AddItem(vm);
                }
            }
            else
            {
                logger.Log(LogFactory.CreateWarningMessage(actionResult.GetComments()));
            }
        }

        private void LaunchCurrentFile()
        {
            if (CurrentFile != null)
            {
                System.Diagnostics.Process.Start(CurrentFile.Path);
            }
        }
        #endregion

        #region events
        #endregion

        #region event handlers
        #endregion


    }
}
