using CygSoft.SmartSession.Desktop.Supports.Validators;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CygSoft.SmartSession.Desktop.Exercises.Edit
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
                this.TargetMetronomeSpeedTextBox.PreviewTextInput += TextBox_PreviewTextInput;
                this.TargetPracticeTimeTextBox.PreviewTextInput += TextBox_PreviewTextInput;
            };
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ValidatorFuncs.TextIsMetronomeSpeed(e.Text);
        }

        private void File_Drop(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //{
            //    // Note that you can have more than one file.
            //    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            //    // Assuming you have one file that you care about, pass it off to whatever
            //    // handling code you have defined.
            //    HandleFileOpen(files);
            //}
        }

        //private void HandleFileOpen(string[] files)
        //{
        //    if (MessageBox.Show(string.Join(Environment.NewLine, files), "Add the folowing files?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //    {
        //        var model = (ExerciseEditViewModel)this.DataContext;
        //        model.AddFiles(files);
        //    }
        //}
    }
}
