namespace JMI.General.VM.IO.Picker
{
    public class DirectoryPickerViewModel : ObservableObject
    {
        public DirectoryPickerViewModel() : this("Directory", "...")
        {
        }

        public DirectoryPickerViewModel(string labelText, string buttonText)
        {
            LabelText = labelText;
            ButtonText = buttonText;
            SelectedPath = string.Empty;
        }

        public string LabelText { get; private set; }
        public string ButtonText { get; private set; }

        private string selectedPath;
        public string SelectedPath
        {
            get { return selectedPath; }
            set { SetProperty(ref selectedPath, value); }
        }
    }
}
