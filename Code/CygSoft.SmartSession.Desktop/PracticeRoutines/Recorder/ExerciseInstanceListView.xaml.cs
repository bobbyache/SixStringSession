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

namespace CygSoft.SmartSession.Desktop.PracticeRoutines.Recorder
{
    /// <summary>
    /// Interaction logic for ExerciseInstanceListView.xaml
    /// </summary>
    public partial class ExerciseInstanceListView : UserControl
    {
        public ExerciseInstanceListView()
        {
            InitializeComponent();
        }

        private void ListViewItem_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // ==================================================================================================================
            // THE COMMENTED CODE BELOW IS WORKING, YOU JUST HAVEN'T FOUND A WAY TO FOCUS ON THE TEXTBOX WITHIN IT FROM CODE !!!
            // ==================================================================================================================

            //var listviewItem = sender as ListViewItem;

            //// https://stackoverflow.com/questions/5750722/how-to-detect-modifier-key-states-in-wpf

            //if (e.Key == Key.Space)
            //    MessageBox.Show("Start or Stop exercise.");

            //if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            //{
            //    // Debugging:
            //    // System.Diagnostics.Debug.WriteLine($"{e.SystemKey} or {e.Key}");

            //    if (e.Key == Key.Left)
            //        MessageBox.Show($"Move progress back");
            //    if (e.Key == Key.Right)
            //        MessageBox.Show($"Move progress forward");

            //    if (e.Key == Key.S)
            //    {
            //        // this does not work:
            //        // https://social.msdn.microsoft.com/Forums/vstudio/en-US/1e9a98c3-b4be-40af-99bd-828491ddcd69/set-focus-of-textbox-in-listview?forum=wpf
            //        MessageBox.Show($"Focus and Highlight current speed.");
            //    }
            //}
        }
    }
}
