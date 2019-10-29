using System;
using System.Collections.ObjectModel;

namespace JMI.General.VM.Commands
{
    public class CommandGroupViewModel
    {
        public CommandGroupViewModel(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName))
            {
                throw new ArgumentNullException(nameof(displayName) + " can not be null or empty.");
            }
            DisplayName = displayName;
            Commands = new ObservableCollection<CommandViewModel>();
        }

        public string DisplayName { get; private set; }

        public ObservableCollection<CommandViewModel> Commands { get; private set; }


    }
}
