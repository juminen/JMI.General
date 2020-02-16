namespace JMI.General.VM.Application
{
    /// <summary>
    /// Provides mechanism for tabs.
    /// </summary>
    public interface ITabViewModel : ICloseViewModel
    {
        /// <summary>
        /// Text displayed in tab header.
        /// </summary>
        string DisplayText { get; set; }
    }
}