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

namespace CygSoft.SmartSession.Desktop.TreeList.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _tree.Model = new PracticeRoutineModel();
        }

        private void _tree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] is Tree.TreeNode)
            {
                var treeNode = e.AddedItems[0] as Tree.TreeNode;
                if (treeNode.Tag is TimeSlot)
                    MessageBox.Show((treeNode.Tag as TimeSlot).Title);
                if (treeNode.Tag is Exercise)
                    MessageBox.Show((treeNode.Tag as Exercise).Title); ;
            }
        }
    }
}
