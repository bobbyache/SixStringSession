using SmartSession.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartSession.Controls
{
    /// <summary>
    /// Interaction logic for SessionRecorderControl.xaml
    /// </summary>
    public partial class SessionRecorderControl : UserControl
    {
        public SessionRecorderControl()
        {
            InitializeComponent();
            this.Loaded += SessionRecorderControl_Loaded;
            DataContext = new SessionListViewModel();
            //SessionComboBox.SelectionChanged += cmbx_SelectionChanged;
        }

        private void SessionRecorderControl_Loaded(object sender, RoutedEventArgs e)
        {
            SessionListViewModel sessionListModel = new SessionListViewModel();
            SessionComboBox.ItemsSource = sessionListModel.Sessions;
            // Visual has been loaded into the tree and is available for use.
        }

        private void Session_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var removedItems = e.RemovedItems;
            var addedItems = e.AddedItems;
            MessageBox.Show(string.Format("Value and Index has been changed to {0}",
                ((SessionViewModel)SessionComboBox.SelectedItem).Title
                ));
        }
    }
}
