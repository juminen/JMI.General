using JMI.General.Logging;
using JMI.General.VM.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace JMI.General.VM.Logging
{
    public class LogViewModel : ObservableObject, IDisposable
    {
        #region constructors
        public LogViewModel()
        {
            messages = new ObservableCollection<LogMessageViewModel>();
            Messages = new ListCollectionView(messages)
            {
                IsLiveSorting = true
            };
            Messages.LiveSortingProperties.Add(nameof(LogMessageViewModel.Time));
            Messages.SortDescriptions.Add(new SortDescription(nameof(LogMessageViewModel.Time), ListSortDirection.Ascending));
            logSystem = Logger.Instance;
            logSystem.MessageReceived += OnLogMessageReceived;
            logSystem.MessagesCleared += OnLogMessagesCleared;
            CreateCommands();
        }
        #endregion

        #region properties
        private readonly Logger logSystem;
        private ObservableCollection<LogMessageViewModel> messages;
        public ListCollectionView Messages { get; private set; }
        public ReadOnlyCollection<CommandGroupViewModel> CommandGroups { get; private set; }
        #endregion

        #region commands
        #endregion

        #region methods
        public void Dispose()
        {
            messages.Clear();
            logSystem.MessageReceived -= OnLogMessageReceived;
            logSystem.MessagesCleared -= OnLogMessagesCleared;
        }

        private void CreateCommands()
        {
            List<CommandViewModel> list = new List<CommandViewModel>();

            RelayCommand clearRelay =
                new RelayCommand(
                    param => logSystem.ClearMessages(),
                    param => logSystem.Messages.Count > 0);
            CommandViewModel clearCvm =
                new CommandViewModel("Clear log", clearRelay);
            list.Add(clearCvm);
            
            RelayCommand copyClipBoardRelay =
                new RelayCommand(
                    param => logSystem.CopyToClipboard(),
                    param => logSystem.Messages.Count > 0);
            CommandViewModel copyClipboardCvm =
                new CommandViewModel("Copy to clipboard", copyClipBoardRelay);
            list.Add(copyClipboardCvm);

            List<CommandGroupViewModel> commandGroupsList = new List<CommandGroupViewModel>();
            CommandGroupViewModel group = new CommandGroupViewModel("Actions");
            foreach (CommandViewModel item in list)
            {
                group.Commands.Add(item);
            }
            commandGroupsList.Add(group);

            CommandGroups = new ReadOnlyCollection<CommandGroupViewModel>(commandGroupsList);
        }
        #endregion

        #region events
        #endregion

        #region event handlers
        private void OnLogMessagesCleared(object sender, EventArgs e)
        {
            messages.Clear();
        }

        private void OnLogMessageReceived(object sender, LogMessageEventArgs e)
        {
            messages.Add(new LogMessageViewModel(e.Message, logSystem.TimeFormat));
        }
        #endregion


    }
}
