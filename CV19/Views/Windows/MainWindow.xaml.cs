using CV19.Models.Decanat;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CV19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void GroupsCollectionFilter(object sender, System.Windows.Data.FilterEventArgs e)
        {
            if (!(e.Item is Group group)) return;
            if (group.Name is null) return;

            var filterText = GroupNameFilterText.Text;
            if (filterText.Length == 0) return;

            if (group.Name.Contains(filterText, System.StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;

        }

        private void OnGroupsFilterChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var text_box = (TextBox)sender;
            var collection = (CollectionViewSource)text_box.FindResource("GroupsCollection");
            collection.View.Refresh();
        }
    }
}
