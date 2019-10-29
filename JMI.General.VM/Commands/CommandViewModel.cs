using System;

namespace JMI.General.VM.Commands
{
    public class CommandViewModel
    {
        public CommandViewModel(string displayName, RelayCommand command)
        {
            if (string.IsNullOrWhiteSpace(displayName))
            {
                throw new ArgumentNullException(nameof(displayName) + " can not be null or empty.");
            }
            DisplayName = displayName;
            Command = command ?? throw new ArgumentNullException(nameof(command) + " can not be null.");
        }

        public string DisplayName { get; private set; }
        public RelayCommand Command { get; private set; }
        public string ToolTip;
    }
}
