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
            DataContext = new SessionListViewModel();
            //SessionComboBox.SelectionChanged += cmbx_SelectionChanged;
        }

        private void Session_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(string.Format("Value and Index has been changed to {0}",
                ((SessionViewModel)SessionComboBox.SelectedItem).Title
                ));
        }
    }
}
