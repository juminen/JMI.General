using System.IO;

namespace JMI.General.IO
{
    /// <summary>
    /// Represents file item
    /// </summary>
    public class File : ObservableObject
    {
        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="PathToFile">Full path to file</param>
        public File(string PathToFile)
        {
            FullPath = PathToFile;
        }
        #endregion

        #region properties

        /// <summary>
        /// File path, eg: "C:\Temp\myfile.pdf"
        /// </summary>
        public string FullPath { get; private set; }
        /// <summary>
        /// Directory path, eg: "C:\Temp"
        /// </summary>
        public string DirectoryPath { get { return Path.GetDirectoryName(FullPath); } }
        /// <summary>
        /// File name with extension, eg: "myfile.pdf"
        /// </summary>
        public string NameWithExtension { get { return Path.GetFileName(FullPath); } }
        /// <summary>
        /// File name without extension, eg: "myfile"
        /// </summary>
        public string NameWithoutExtension { get { return Path.GetFileNameWithoutExtension(FullPath); } }
        /// <summary>
        /// File extension eg: ".pdf". If there is no extension, returns empty string.
        /// </summary>
        public string Extension
        { get
            {
                if (Path.HasExtension(FullPath))
                {
                    return Path.GetExtension(FullPath);
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// File type (extension without period) eg: "pdf". If there is no extension, returns empty string.
        /// </summary>
        public string Type
        {
            get
            {
                if (Path.HasExtension(FullPath))
                {
                    return Path.GetExtension(FullPath).Substring(1);
                }
                return string.Empty;
            }
        }

        //TODO: add creation time etc.
        #endregion

        #region commands
        #endregion

        #region methods
        /// <summary>
        /// Check file existence
        /// </summary>
        /// <returns>true if file exists</returns>
        public bool Exists()
        {
            return System.IO.File.Exists(FullPath);
        }
        #endregion

        #region events
        #endregion

        #region event handlers
        #endregion
    }
}
