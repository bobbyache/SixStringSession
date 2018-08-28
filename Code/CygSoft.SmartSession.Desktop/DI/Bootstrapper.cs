using Castle.Windsor;
using Castle.Windsor.Installer;
using System;

namespace CygSoft.SmartSession.Desktop.DI
{
    public class Bootstrapper
    {
        private static volatile IWindsorContainer _theWindsorContainer;
        private static object syncRoot = new Object();

        public static IWindsorContainer Container
        {
            get
            {
                if (_theWindsorContainer == null)
                {
                    lock (syncRoot)
                    {
                        if (_theWindsorContainer == null)
                        {
                            _theWindsorContainer = new WindsorContainer().Install(FromAssembly.This());
                        }
                    }
                }

                return _theWindsorContainer;
            }
        }
    }
}
