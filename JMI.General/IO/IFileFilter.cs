namespace JMI.General.IO
{
    /// <summary>
    /// Helper used in file dialog filters
    /// </summary>
    public interface IFileFilter
    {
        /// <summary>
        /// Description shown in open and save file dialog boxes.<br/>
        /// Example: "PDF files"
        /// </summary>
        string Description { get; }
        /// <summary>
        /// File extension withoud period.<br/>
        /// Example: "pdf"
        /// </summary>
        string FileType { get; }
        /// <summary>
        /// Returns file description and extension in filedialox format, <see cref="FileDialog.Filter"/>.<br/>
        /// Example: "PDF files (*.pdf)|*.pdf"
        /// </summary>
        string Filter { get; }
    }
}
