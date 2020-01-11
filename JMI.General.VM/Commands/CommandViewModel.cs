using System;

namespace JMI.General.VM.Commands
{
    /// <summary>
    /// Used in user interface for commands (menus, buttons etc.)
    /// </summary>
    public class CommandViewModel
    {
        /// <summary>
        /// Default constuctor
        /// </summary>
        /// <param name="displayName">Display</param>
        /// <param name="command"></param>
        public CommandViewModel(string displayName, RelayCommand command)
        {
            if (string.IsNullOrWhiteSpace(displayName))
            {
                throw new ArgumentNullException(nameof(displayName) + " can not be null or empty.");
            }
            DisplayName = displayName;
            Command = command ?? throw new ArgumentNullException(nameof(command) + " can not be null.");
        }

        /// <summary>
        /// Text displayed in user interface.
        /// </summary>
        public string DisplayName { get; private set; }
        /// <summary>
        /// Command that is connected to user interface element.
        /// </summary>
        public RelayCommand Command { get; private set; }
        /// <summary>
        /// Tooltip shown to user
        /// </summary>
        public string ToolTip { get; set; }
    }
}
