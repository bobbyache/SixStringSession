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
using System.Windows.Shapes;

namespace SliderControl
{
    /// <summary>
    /// Interaction logic for Slider_Listview_Window.xaml
    /// </summary>
    public partial class Slider_Listview_Window : Window
    {
        public Slider_Listview_Window()
        {
            InitializeComponent();
        }

        private void RecordableExerciseItemCtrl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var item = ((ListBoxItem)sender).Content as SomeTemplateViewModel;

            if (item == null)
                return;

            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                // Debugging:
                // System.Diagnostics.Debug.WriteLine($"{e.SystemKey} or {e.Key}");

                if (e.Key == Key.Left)
                    item.DecreaseManualProgressCommand.Execute(null);

                if (e.Key == Key.Right)
                    item.IncreaseManualProgressCommand.Execute(null);
            }
        }
    }
}
