using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TestApp.Common.Source.View.Models;
using TestApp.Common.Source.Wifi;
using TestApp.Droid.Source.Adapter.Wifi;
using TinyIoC;

namespace TestApp.Droid.Source.Utilities
{
    class ViewModelLocator
    {
        private static ViewModelLocator _inst;

        private TinyIoCContainer _container;

        public static ViewModelLocator Locator
        {
            get
            {
                if (_inst != null)
                {
                    return _inst;
                }

                _inst = new ViewModelLocator();
                _inst.Init();
                Debug.WriteLine($"{nameof(ViewModelLocator)} created");

                return _inst;
            }
        }

        public MainViewModel MainViewModel => _container.Resolve<MainViewModel>();

        public ConnectToWifiViewModel ConnectToWifiViewModel => _container.Resolve<ConnectToWifiViewModel>();

        public ITestAppWifiService TestAppWifiService => _container.Resolve<ITestAppWifiService>();

        public void Init()
        {
            _container = TinyIoCContainer.Current;
            var container = _container;
            var context = global::Android.App.Application.Context.ApplicationContext;

            // services
            var wifi = new TestAppWifiService();
            container.Register<ITestAppWifiService>(wifi);

            container.Register<MainViewModel>();
            container.Register<ConnectToWifiViewModel>();
        }
    }
}