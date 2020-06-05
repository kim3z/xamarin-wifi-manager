using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net.Wifi;
using Android.Support.V4.Content;
using TestApp.Common.Source.Wifi;

namespace TestApp.Droid.Source.Adapter.Wifi
{
    class TestAppWifiService : ITestAppWifiService
    {
        private static WifiManager _wifiManager;

        public ObservableCollection<IWifiNetwork> WifiNetworks { get; set; }

        private CancellationTokenSource _continuousScanNetworksCancellationTokenSource;

        public TestAppWifiService()
        {
            WifiNetworks = new ObservableCollection<IWifiNetwork>();
        }

        public void ScanWifiNetworksContinuously()
        {
            _continuousScanNetworksCancellationTokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (true)
                {
                    System.Diagnostics.Debug.WriteLine("Searching...");
                    WifiNetworks.Clear();
                    _continuousScanNetworksCancellationTokenSource.Token.ThrowIfCancellationRequested();
                    var ctx = Application.Context;
                    var wifiMonitor = new WifiMonitor();
                    wifiMonitor.OnNetworkDetected += WifiMonitor_OnNetworkDetected;
                    ctx.RegisterReceiver(wifiMonitor, new IntentFilter(WifiManager.ScanResultsAvailableAction));
                    _wifiManager = ((WifiManager)Application.Context.GetSystemService(Context.WifiService));
                    _wifiManager.StartScan();

                    await Task.Delay(5000);
                }
            });
        }

        public void StopScanning()
        {
            try
            {
                _continuousScanNetworksCancellationTokenSource.Cancel();
            }
            catch (OperationCanceledException)
            {
                //
            }
        }

        private bool CanAccessWifi()
        {
            return ContextCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessWifiState) == (int)Permission.Granted;
        }

        private bool CanChangeWifi()
        {
            return ContextCompat.CheckSelfPermission(Application.Context, Manifest.Permission.ChangeWifiState) == (int)Permission.Granted;
        }

        private void WifiMonitor_OnNetworkDetected(object sender, OnNetworkDetectedEventArgs e)
        {
            WifiNetworks.Add(e.WifiNetwork);
        }

        public class WifiMonitor : BroadcastReceiver
        {
            public event EventHandler<OnNetworkDetectedEventArgs> OnNetworkDetected;

            public override void OnReceive(Context context, Intent intent)
            {
                IList<ScanResult> scanwifinetworks = _wifiManager.ScanResults;
                foreach (ScanResult n in scanwifinetworks)
                {
                    // System.Diagnostics.Debug.WriteLine($"Detected network: {n.Ssid} - {n.Bssid}");
                    var network = new WifiNetwork()
                    {
                        Ssid = n.Ssid,
                        Bssid = n.Bssid,
                        Level = n.Level
                    };

                    var args = new OnNetworkDetectedEventArgs()
                    {
                        WifiNetwork = network
                    };
                    OnNetworkDetected.Invoke(this, args);
                }
            }
        }

        public class OnNetworkDetectedEventArgs : EventArgs
        {
            public IWifiNetwork WifiNetwork { get; set; }
        }
    }
}