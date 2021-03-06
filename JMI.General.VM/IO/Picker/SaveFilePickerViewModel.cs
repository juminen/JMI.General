﻿namespace JMI.General.VM.IO.Picker
{
    public class SaveFilePickerViewModel : ObservableObject
    {
        public SaveFilePickerViewModel() : this("File", "...")
        {
        }

        public SaveFilePickerViewModel(string labelText, string buttonText)
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

        private string fileFilters;
        public string FileFilters
        {
            get { return fileFilters; }
            set { SetProperty(ref fileFilters, value); }
        }
    }
}
