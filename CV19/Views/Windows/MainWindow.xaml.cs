using CV19.Models.Decanat;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CV19
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CollectionViewSource_Filter(object sender, System.Windows.Data.FilterEventArgs e)
        {
            if(!(e.Item is Group group)) return;
            if(group.Name is null) return;

            var filter_text = GroupNameFilterText.Text;
            if (filter_text.Length == 0) return;

            if(group.Name.Contains(filter_text, System.StringComparison.OrdinalIgnoreCase)) return;
            if(group.Description is not null && group.Description.Contains(filter_text, System.StringComparison.OrdinalIgnoreCase)) return;
            
            e.Accepted = false;
        }

        private void OnGroupsFilterTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var collection = (CollectionViewSource)textBox.FindResource("GroupsCollection");
            collection.View.Refresh();
        }
    }
}
