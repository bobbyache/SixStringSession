using Caliburn.Micro;
using JsonDb;
using System;
using System.Windows;

namespace SmartGoals
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        public ShellViewModel(ExampleViewModel exampleViewModel)
        {
            ExampleViewModel = exampleViewModel;
        }

        public ExampleViewModel ExampleViewModel { get; }
    }
}