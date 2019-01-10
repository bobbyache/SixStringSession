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
    /// Interaction logic for Slider_A_Window.xaml
    /// </summary>
    public partial class Slider_A_Window : Window
    {
        public Slider_A_Window()
        {
            InitializeComponent();
            this.Loaded += Slider_A_Window_Loaded;
        }

        private void Slider_A_Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
