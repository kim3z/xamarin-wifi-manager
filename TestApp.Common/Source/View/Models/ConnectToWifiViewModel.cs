using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Common.Source.Wifi;

namespace TestApp.Common.Source.View.Models
{
    public class ConnectToWifiViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly ITestAppWifiService _testAppWifiService;

        public ConnectToWifiViewModel(ITestAppWifiService testAppWifiService)
        {
            _testAppWifiService = testAppWifiService;
        }

        public void GetWifiNetWorks()
        {
            var foo = _testAppWifiService.GetWifiNetworks();
        }
    }
}
