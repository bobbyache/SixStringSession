using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CygSoft.SmartSession.Desktop.Exercises.Management
{
    public partial class ExerciseManagementView : UserControl
    {
        public ExerciseManagementView()
        {
            InitializeComponent();
            this.MouseDown += MainWindow_MouseDown;
            this.MouseDoubleClick += MainWindow_MouseDoubleClick;
            this.PreviewDragOver += MainWindow_PreviewDragOver;
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                try
                {
                    ContextMenu cm = this.FindResource("contextMenu") as ContextMenu;
                    cm.PlacementTarget = sender as Window;
                    cm.IsOpen = true;
                }
                catch { /* TODO:Find out why this exception occurs */ }
            }
        }

        private void MainWindow_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            //throw new NotImplementedException();
        }

        private void MainWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            //MessageBox.Show("Here you go!");
        }

    }
}
