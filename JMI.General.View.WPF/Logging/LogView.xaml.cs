using System.Collections.Specialized;
using System.Windows.Controls;

namespace JMI.General.View.WPF.Logging
{
    /// <summary>
    /// Interaction logic for LogView.xaml
    /// </summary>
    public partial class LogView : UserControl
    {
        public LogView()
        {
            InitializeComponent();
            ((INotifyCollectionChanged)LogGrid.Items).CollectionChanged += OnLogViewCollectionChanged;
        }
        private void OnLogViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (LogGrid.Items.Count > 0)
            {
                LogGrid.ScrollIntoView(LogGrid.Items[LogGrid.Items.Count - 1]);
            }
        }
    }
}
