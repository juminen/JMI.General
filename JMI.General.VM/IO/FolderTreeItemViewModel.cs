using JMI.General.Trees;
using System;
using System.IO;

namespace JMI.General.VM.IO
{
    public class FolderTreeItemViewModel : TreeItem
    {
        #region constructors
        public FolderTreeItemViewModel(DirectoryInfo directoryInfo)
        {
            folder = directoryInfo ?? throw new ArgumentNullException(nameof(directoryInfo));
        }
        #endregion

        #region properties
        private DirectoryInfo folder;

        public override string DisplayName { get { return folder.Name; } }
        public override string Description { get { return folder.Name; } }
        public override string Path { get { return folder.FullName; } }
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
