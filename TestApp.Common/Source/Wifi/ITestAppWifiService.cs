using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Common.Source.Wifi
{
    public interface ITestAppWifiService
    {
        ObservableCollection<IWifiNetwork> WifiNetworks { get; set;  }

        void ScanWifiNetworksContinuously();

        void StopScanning();
    }
}
