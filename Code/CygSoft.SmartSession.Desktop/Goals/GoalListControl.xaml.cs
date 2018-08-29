using System.Windows.Controls;

namespace CygSoft.SmartSession.Desktop.Goals
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
