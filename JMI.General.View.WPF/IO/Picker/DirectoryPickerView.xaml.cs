using System.Windows;
using System.Windows.Forms;

namespace JMI.General.View.WPF.IO.Picker
{
    /// <summary>
    /// Interaction logic for DirectoryPickerView.xaml
    /// </summary>
    public partial class DirectoryPickerView : System.Windows.Controls.UserControl
    {
        public DirectoryPickerView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                SelectedPathTextBox.Text = fbd.SelectedPath;
            }
        }
    }
}