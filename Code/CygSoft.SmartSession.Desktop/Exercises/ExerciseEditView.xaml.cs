using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CygSoft.SmartSession.Desktop.Exercises
{
    /// <summary>
    /// Interaction logic for ExerciseEditView.xaml
    /// </summary>
    public partial class ExerciseEditView : UserControl
    {
        public ExerciseEditView()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                this.OptimalDurationTextBox.PreviewTextInput += OptimalDurationTextBox_PreviewTextInput;
            };
        }

        private void OptimalDurationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string onlyNumeric = @"^([0-9]+(.[0-9]+)?)$";
            Regex regex = new Regex(onlyNumeric);
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
