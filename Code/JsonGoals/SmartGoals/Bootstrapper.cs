using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SmartGoals
{
    /*
     * Live Charts: https://lvcharts.net/
     * WPF in C# with MVVM using Caliburn Micro: https://www.youtube.com/watch?v=laPFq3Fhs8k
     * Documentation: https://caliburnmicro.com/documentation/
     * Caliburn Micro Introduction Series: https://www.youtube.com/watch?v=vVFXQ1fvFTc&list=PL3JeBX8MKjuHhSFbPOwbrxvdiRC1Lsrkb
     * */

    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer simpleContainer = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override object GetInstance(Type service, string key)
        {
            return simpleContainer.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return simpleContainer.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            simpleContainer.BuildUp(instance);
        }

        protected override void Configure()
        {
            simpleContainer.Singleton<IWindowManager, WindowManager>();
            simpleContainer.Singleton<ShellViewModel>();
            simpleContainer.Singleton<HeaderViewModel>();
            simpleContainer.Singleton<ContentViewModel>();
            simpleContainer.Singleton<GreetingsMessageProvider>();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            base.OnExit(sender, e);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            // https://stackoverflow.com/questions/16404538/setting-the-initial-window-size-in-caliburn-micro

            //double width = 1000; // Settings.Default.screen_width;  //Previous window width 
            //double height = 800; //  Settings.Default.screen_height; //Previous window height

            //double screen_width = System.Windows.SystemParameters.PrimaryScreenWidth;
            //double screen_height = System.Windows.SystemParameters.PrimaryScreenHeight;

            //if (width > screen_width) width = (screen_width - 10);
            //if (height > screen_height) height = (screen_height - 10);

            //Dictionary<string, object> window_settings = new Dictionary<string, object>();

            //window_settings.Add("Width", screen_width);
            //window_settings.Add("Height", screen_height);

            // DisplayRootViewFor<ShellViewModel>(window_settings);

            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
