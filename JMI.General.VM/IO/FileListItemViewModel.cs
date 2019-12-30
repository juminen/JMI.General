using JMI.General.ListSelection;
using System;
using System.IO;

namespace JMI.General.VM.IO
{
    public class FileListItemViewModel : SelectionCollectionItem
    {
        #region constructors
        public FileListItemViewModel(FileInfo currentFile)
        {
            file = currentFile ?? throw new ArgumentNullException(nameof(currentFile));
            OnPropertyChanged("");
        }

        public FileListItemViewModel(FileInfo currentFile, string timeFormat)
            : this(currentFile)
        {
            this.timeFormat = timeFormat;
        }
        #endregion

        #region properties
        private FileInfo file;
        private readonly string timeFormat = "yyyy-MM-dd HH:ss";

        public string Path { get { return file.FullName; } }
        public override string Id { get { return Path; } }
        public override string DisplayText { get { return FileName; } }

        public string FileName { get { return file.Name; } }
        public string FileType { get { return file.Extension.ToUpper().Substring(1); } }
        public string Created { get { return file.CreationTime.ToString(timeFormat); } }
        public string Modified { get { return file.LastWriteTime.ToString(timeFormat); } }
        public string Accessed { get { return file.LastAccessTime.ToString(timeFormat); } }
        public string Size { get { return SizeSuffix(file.Length); } }

        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        static string SizeSuffix(long value, int decimalPlaces = 1)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }

            int i = 0;
            decimal dValue = value;
            while (Math.Round(dValue, decimalPlaces) >= 1000)
            {
                dValue /= 1024;
                i++;
            }
            return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
        }

        public override void Dispose()
        {
            
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
