using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JMI.General.View.WPF.IO.Picker
{
    /// <summary>
    /// Interaction logic for DirectoryPicker.xaml
    /// </summary>
    public partial class DirectoryPicker : System.Windows.Controls.UserControl
    {
        public DirectoryPicker()
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
            DependencyProperty.Register("Label", typeof(string), typeof(DirectoryPicker), new PropertyMetadata("Path:"));

        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }

        public static readonly DependencyProperty SelectedPathProperty =
            DependencyProperty.Register("SelectedPath", typeof(string), typeof(DirectoryPicker), new PropertyMetadata(""));

        public string ButtonLabel
        {
            get { return (string)GetValue(ButtonLabelProperty); }
            set { SetValue(ButtonLabelProperty, value); }
        }

        public static readonly DependencyProperty ButtonLabelProperty =
            DependencyProperty.Register("ButtonLabel", typeof(string), typeof(DirectoryPicker), new PropertyMetadata("..."));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO: etsi jostain parempi 
            //SelectDirectoryWin32();
            //SelectDirectoryWinFormsOpenFileDialog();
            SelectDirectoryWinForms();
        }

        private void SelectDirectoryWinForms()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                SelectedPath = fbd.SelectedPath;
            }
        }

        private void SelectDirectoryWin32()
        {
            //Ei toimi, open-napin painaminen ei valitse hakemistoa.
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = false,
                AddExtension = false,
                CheckFileExists = false,
                CheckPathExists = true,
                Filter = "Directories|*.none",
                FileName="Select folder and press open"
            };
            if (ofd.ShowDialog() == true)
            {
                SelectedPath = System.IO.Path.GetDirectoryName(ofd.FileName);
            }
        }

        private void SelectDirectoryWinFormsOpenFileDialog()
        {
            //Ei toimi, open-napin painaminen ei valitse hakemistoa.
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog
            {
                Multiselect = false,
                AddExtension = false,
                CheckFileExists = false,
                CheckPathExists = true,
                Filter = "Folders|\n"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SelectedPath= ofd.FileName;
            }
        }
    }
}
