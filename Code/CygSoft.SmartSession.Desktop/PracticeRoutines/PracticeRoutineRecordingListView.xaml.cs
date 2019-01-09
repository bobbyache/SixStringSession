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

        private void ListViewItem_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // https://stackoverflow.com/questions/5750722/how-to-detect-modifier-key-states-in-wpf

            if (e.Key == Key.Space)
                MessageBox.Show("Start or Stop exercise.");

            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                // Debugging:
                // System.Diagnostics.Debug.WriteLine($"{e.SystemKey} or {e.Key}");

                if (e.Key == Key.Left)
                    MessageBox.Show($"Move progress back");
                if (e.Key == Key.Right)
                    MessageBox.Show($"Move progress forward");

                if (e.Key == Key.S)
                {
                    MessageBox.Show($"Focus and Highlight current speed.");
                }
            }

            //// Use this to execute commands!
            //// -------------------------------------------------------------------
            ////var viewModel = DataContext as YourViewModel;
            ////viewModel.YourCommand.Execute(null);
        }
    }
}
