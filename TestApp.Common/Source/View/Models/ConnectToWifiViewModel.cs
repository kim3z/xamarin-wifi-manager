using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Common.Source.Wifi;

namespace TestApp.Common.Source.View.Models
{
    public class ConnectToWifiViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly ITestAppWifiService _testAppWifiService;
        public List<IWifiNetwork> WifiNetworks { get; set; }

        public ConnectToWifiViewModel(ITestAppWifiService testAppWifiService)
        {
            _testAppWifiService = testAppWifiService;
            WifiNetworks = new List<IWifiNetwork>();
            _testAppWifiService.WifiNetworks.CollectionChanged += WifiNetworks_CollectionChanged;
        }

        private void WifiNetworks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var newWifiItems = e.NewItems;

            if (newWifiItems == null)
            {
                return;
            }
            var shouldUpdate = false;
            foreach (var wifiNetwork in newWifiItems)
            {
                if (!(wifiNetwork is IWifiNetwork network))
                {
                    continue;
                }

                var notInWifiList = WifiNetworks.All(x => x.Ssid != network.Ssid);
                if (notInWifiList)
                {
                    WifiNetworks.Add(network);
                    shouldUpdate = true;
                } else
                {
                    // update network info
                    foreach (var existingNetwork in WifiNetworks)
                    {
                        if (existingNetwork.Bssid == network.Bssid && existingNetwork.Level != network.Level)
                        {
                            existingNetwork.Level = network.Level;
                            shouldUpdate = true;
                        }
                    }

                }
            }
            
            if (shouldUpdate)
            {
                SortNetworks();
                RaisePropertyChanged(() => WifiNetworks);
            }
        }

        private void SortNetworks()
        {
            WifiNetworks.OrderBy(x => x.Level);
        }

        public void ScanWifiNetworksContinuously()
        {
            WifiNetworks.Clear();

            _testAppWifiService.ScanWifiNetworksContinuously();
        }

        public void StopScanningWifiNetworks()
        {
            WifiNetworks.Clear();

            _testAppWifiService.StopScanning();
        }
    }
}
