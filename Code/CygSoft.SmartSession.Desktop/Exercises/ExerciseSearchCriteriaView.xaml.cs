using CygSoft.SmartSession.Desktop.Supports.Validators;
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
                this.TargetMetronomeSpeedTextBox.PreviewTextInput += TargetMetronomeSpeedTextBox_PreviewTextInput;
                this.TargetPracticeTimeTextBox.PreviewTextInput += TargetPracticeTimeTextBox_PreviewTextInput;
            };
        }

        private void TargetMetronomeSpeedTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ValidatorFuncs.TextIsMetronomeSpeed(e.Text);
        }

        private void TargetPracticeTimeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ValidatorFuncs.TextIsInteger(e.Text);
        }
    }
}
