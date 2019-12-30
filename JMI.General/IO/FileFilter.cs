using System;

namespace JMI.General.IO
{
    public class FileFilter : IFileFilter
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fileDescription"><see cref="Description"/></param>
        /// <param name="fileExtension">><see cref="FileType"/></param>
        public FileFilter(string fileDescription, string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileDescription))
            {
                throw new ArgumentException($"{nameof(fileDescription)} cannot be empty or contain white spaces");
            }
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                throw new ArgumentException($"{nameof(fileExtension)} cannot be empty or contain white spaces");
            }
            Description = fileDescription;
            FileType = fileExtension;
        }

        public string Description { get; }
        public string FileType { get; }
        public string Filter
        {
            get
            {
                //Filter = "dwg files (*.dwg)|*.dwg"
                return $"{Description} (*.{FileType.ToLower()})|*.{FileType.ToLower()}";
            }
        }
    }
}
