﻿using MvvmLight_Prototypes.ViewModel;
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

namespace MvvmLight_Prototypes
{
  /// <summary>
  /// Interaction logic for GoalListControl.xaml
  /// </summary>
  public partial class GoalListControl : UserControl
  {
    public GoalListControl()
    {
      InitializeComponent();
      this.DataContext = new GoalListViewModel();
    }
  }
}
