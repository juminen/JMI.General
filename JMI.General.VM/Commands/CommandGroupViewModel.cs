using System;
using System.Collections.ObjectModel;

namespace JMI.General.VM.Commands
{
    /// <summary>
    /// Used in user interface to group <see cref="CommandViewModel"/>.
    /// </summary>
    public class CommandGroupViewModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="displayName"><see cref="DisplayName"/></param>
        public CommandGroupViewModel(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName))
            {
                throw new ArgumentNullException(nameof(displayName) + " can not be null or empty.");
            }
            DisplayName = displayName;
            Commands = new ObservableCollection<CommandViewModel>();
        }

        /// <summary>
        /// Text displayed in user interface.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Collection of <see cref="CommandViewModel"/>.
        /// </summary>
        public ObservableCollection<CommandViewModel> Commands { get; private set; }


    }
}
