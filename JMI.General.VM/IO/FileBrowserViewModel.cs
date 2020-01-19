using JMI.General.Logging;
using System;
using System.IO;

namespace JMI.General.VM.IO
{
    public class FileBrowserViewModel : ObservableObject
    {
        #region constructors
        public FileBrowserViewModel()
        {
            Tree = new DirectoryTreeViewModel();
            Tree.CurrentNodeChanged += OnTreeCurrentNodeChanged;
            Folders = new FolderListViewModel();
            Files = new FileListViewModel();
            Tree.LoadRootNodes();
        }
        #endregion

        #region properties
        private SingletonLogger logger = SingletonLogger.Instance;
        public DirectoryTreeViewModel Tree { get; private set; }
        public FolderListViewModel Folders { get; private set; }
        public FileListViewModel Files { get; private set; }

        private string currentPath;
        public string CurrentPath
        {
            get { return currentPath; }
            set
            {
                if (SetProperty(ref currentPath, value))
                {
                    OnPropertyChanged(nameof(GotoEnabled));
                }
            }
        }

        public bool GotoEnabled { get { return Directory.Exists(CurrentPath); } }

        //public string CurrentItemPath
        //{
        //    get
        //    {
        //        if (Tree.CurrentNode == null)
        //        {
        //            return string.Empty;
        //        }
        //        return Tree.CurrentNode.Path;
        //    }
        //}
        #endregion

        #region commands
        private RelayCommand loadTreeCommand;
        public RelayCommand LoadTreeCommand
        {
            get
            {
                if (loadTreeCommand == null)
                {
                    loadTreeCommand =
                        new RelayCommand(
                            param => Tree.LoadRootNodes(),
                            param => Tree != null && !Tree.IsLoadingNodes);
                }
                return loadTreeCommand;
            }
        }

        private RelayCommand gotoPathCommand;
        public RelayCommand GotoPathCommand
        {
            get
            {
                if (gotoPathCommand == null)
                {
                    gotoPathCommand =
                        new RelayCommand(
                            param => Tree.SelectTreeItem(CurrentPath),
                            param => GotoEnabled);
                }
                return gotoPathCommand;
            }
        }

        #endregion

        #region methods
        private void GetCurrentNodeFilesAndFolders()
        {
            Folders.ChangePath(CurrentPath);
            Files.ChangePath(CurrentPath);
        }

        private void SetCurrentItemPath()
        {
            if (Tree.CurrentNode != null)
            {
                CurrentPath = Tree.CurrentNode.Path;
            }
            else
            {
                CurrentPath = string.Empty;
            }
        }

        //private void GotoPath()
        //{
        //    if (CurrentPath.Equals(Tree.CurrentNode.Path))
        //    {
        //        return;
        //    }
        //    ;
        //}
        #endregion

        #region events
        #endregion

        #region event handlers
        private void OnTreeCurrentNodeChanged(object sender, EventArgs e)
        {
            //OnPropertyChanged(nameof(CurrentItemPath));
            SetCurrentItemPath();
            GetCurrentNodeFilesAndFolders();
        }
        #endregion
    }
}
