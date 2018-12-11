using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace CygSoft.SmartSession.Desktop.Controls.GuiTests
{
    /// <summary>
    /// Interaction logic for MyTextControlWindow.xaml
    /// </summary>
    public partial class MyTextControlWindow : Window
    {
        public MyTextControlWindow()
        {
            InitializeComponent();
            DataContext = new DataObject() { Name = "Rob Blake" }; ;
        }

        public class DataObject : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private string name;

            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }

            protected void NotifyPropertyChanged(string propertyName)
            {
                var handler = PropertyChanged;
                if (handler != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
