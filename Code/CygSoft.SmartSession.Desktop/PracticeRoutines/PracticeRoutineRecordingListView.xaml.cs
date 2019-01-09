using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    /// <summary>
    /// Interaction logic for PracticeRoutineRecordingListView.xaml
    /// </summary>
    public partial class PracticeRoutineRecordingListView : UserControl
    {
        public PracticeRoutineRecordingListView()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Slider_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
