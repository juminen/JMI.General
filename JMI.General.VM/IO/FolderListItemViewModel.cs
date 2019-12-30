using JMI.General.ListSelection;
using System;
using System.IO;

namespace JMI.General.VM.IO
{
    public class FolderListItemViewModel : SelectionCollectionItem
    {
        #region constructors
        public FolderListItemViewModel(DirectoryInfo currentFolder)
        {
            folder = currentFolder ?? throw new Exception(nameof(currentFolder) + " can not be null");
            OnPropertyChanged("");
        }

        public FolderListItemViewModel(DirectoryInfo currentFolder, string timeFormat)
            : this(currentFolder)
        {
            this.timeFormat = timeFormat;
        }
        #endregion

        #region properties
        private DirectoryInfo folder;
        private readonly string timeFormat = "yyyy-MM-dd HH:ss";

        public override string Id { get { return DirectoryName; } }
        public override string DisplayText { get { return DirectoryName; } }

        public string Path { get { return folder.FullName; } }
        public string DirectoryName { get { return folder.Name; } }
        public string Created { get { return folder.CreationTime.ToString(timeFormat); } }


        public override void Dispose()
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region commands
        #endregion

        #region methods
        #endregion

        #region events
        #endregion

        #region event handlers
        #endregion
    }
}
