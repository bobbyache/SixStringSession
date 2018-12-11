using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CygSoft.SmartSession.Desktop.Controls
{
    public class MyTextControl : Control
    {
        static MyTextControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(MyTextControl), new FrameworkPropertyMetadata(typeof(MyTextControl)
                    ));
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }   

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MyTextControl), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    }
}
