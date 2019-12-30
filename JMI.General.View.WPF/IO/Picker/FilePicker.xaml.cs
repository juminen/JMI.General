using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace JMI.General.View.WPF.IO.Picker
{
    /// <summary>
    /// Interaction logic for FilePicker.xaml
    /// </summary>
    public partial class FilePicker : UserControl
    {
        public FilePicker()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(FilePicker), new PropertyMetadata("Path:"));

        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }

        public static readonly DependencyProperty SelectedPathProperty =
            DependencyProperty.Register("SelectedPath", typeof(string), typeof(FilePicker), new PropertyMetadata(""));

        public string ButtonLabel
        {
            get { return (string)GetValue(ButtonLabelProperty); }
            set { SetValue(ButtonLabelProperty, value); }
        }

        public static readonly DependencyProperty ButtonLabelProperty =
            DependencyProperty.Register("ButtonLabel", typeof(string), typeof(FilePicker), new PropertyMetadata("..."));

        public string FileFilters
        {
            get { return (string)GetValue(FileFiltersProperty); }
            set { SetValue(FileFiltersProperty, value); }
        }

        public static readonly DependencyProperty FileFiltersProperty =
            DependencyProperty.Register("FileFilters", typeof(string), typeof(FilePicker), new PropertyMetadata(""));

        public PickerMode Mode
        {
            get { return (PickerMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(PickerMode), typeof(FilePicker), new PropertyMetadata(PickerMode.OpenSingleFile));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (Mode)
            {
                case PickerMode.OpenSingleFile:
                    SelectOpenSingleFile();
                    break;
                case PickerMode.SaveFile:
                    SelectSaveFile();
                    break;
                default:
                    break;
            }
        }

        private void SelectOpenSingleFile()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = false,
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = FileFilters
            };
            if (ofd.ShowDialog() == true)
            {
                SelectedPath = ofd.FileName;
            }
        }

        private void SelectSaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                AddExtension = true,
                CheckPathExists = true,
                OverwritePrompt = true,
                Filter = FileFilters
            };
            if (sfd.ShowDialog() == true)
            {
                SelectedPath = sfd.FileName;
            }
        }
    }

    public enum PickerMode
    {
        OpenSingleFile,
        SaveFile,
    }
}
