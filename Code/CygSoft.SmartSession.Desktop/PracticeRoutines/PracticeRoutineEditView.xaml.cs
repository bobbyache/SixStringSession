﻿using CygSoft.SmartSession.Desktop.Supports.Validators;
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

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    /// <summary>
    /// Interaction logic for PracticeRoutineEditView.xaml
    /// </summary>
    public partial class PracticeRoutineEditView : UserControl
    {
        public PracticeRoutineEditView()
        {
            InitializeComponent();
            this.MinutesTextBox.PreviewTextInput += TextBox_PreviewTextInput;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ValidatorFuncs.TextIsInteger(e.Text);
        }
    }
}
