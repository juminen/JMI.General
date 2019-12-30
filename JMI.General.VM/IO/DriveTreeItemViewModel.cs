using JMI.General.Trees;
using System;
using System.IO;

namespace JMI.General.VM.IO
{
    public class DriveTreeItemViewModel : TreeItem
    {
        #region constructors
        public DriveTreeItemViewModel(DriveInfo driveInfo)
        {
            drive = driveInfo ?? throw new ArgumentNullException(nameof(driveInfo));
        }
        #endregion

        #region properties
        private DriveInfo drive;

        public override string DisplayName { get { return drive.Name; } }
        public override string Description { get { return drive.Name; } }
        public override string Path { get { return drive.Name; } }
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
