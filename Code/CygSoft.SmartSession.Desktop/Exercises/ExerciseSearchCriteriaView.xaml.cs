using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    /// <summary>
    /// Interaction logic for ExerciseSearchCriteriaView.xaml
    /// </summary>
    public partial class ExerciseSearchCriteriaView : UserControl
    {
        public ExerciseSearchCriteriaView()
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
