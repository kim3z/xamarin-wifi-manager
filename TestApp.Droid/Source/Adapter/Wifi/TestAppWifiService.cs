using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using TestApp.Common.Source.Wifi;

namespace TestApp.Droid.Source.Adapter.Wifi
{
    class TestAppWifiService : ITestAppWifiService
    {
        List<IWifiNetwork> _wifiNetworks;

        public TestAppWifiService()
        {
            System.Diagnostics.Debug.WriteLine("TEST APP WIFI SERVICE");
            GetNetworks();
        }

        private void GetNetworks()
        {
            if (CanAccessWifi() && CanChangeWifi())
            {
                System.Diagnostics.Debug.WriteLine("ENABLED");
                // We have permission, go ahead and use the camera.
                WifiManager wifiManager = (WifiManager)Application.Context.GetSystemService(Context.WifiService);
                if (!wifiManager.IsWifiEnabled)
                {
                    wifiManager.SetWifiEnabled(true);
                }
                else
                {
                    // wifiManager.SetWifiEnabled(false);
                }

                var networks = new List<IWifiNetwork>();

                foreach (var n in wifiManager.ConfiguredNetworks)
                {
                    var network = new WifiNetwork()
                    {
                        Ssid = n.Ssid,
                        HiddenSSID = n.HiddenSSID,
                        NetworkId = n.NetworkId,
                        ProvideFriendlyName = n.ProviderFriendlyName
                    }
                    System.Diagnostics.Debug.WriteLine($"=== WifiNetwork : {n.Ssid} ===");
                    System.Diagnostics.Debug.WriteLine($"=== WifiNetwork : {n.HiddenSSID} ===");
                    System.Diagnostics.Debug.WriteLine($"=== WifiNetwork : {n.NetworkId} ===");
                    System.Diagnostics.Debug.WriteLine($"=== WifiNetwork : {n.ProviderFriendlyName} ===");
                }
            }
            else
            {
                // Wifi permission is not granted. If necessary display rationale & request.
                System.Diagnostics.Debug.WriteLine("NOT GRANTED");
            }
        }

        public Task<List<IWifiNetwork>> GetWifiNetworks()
        {
            return null;
        }

        private bool CanAccessWifi()
        {
            return ContextCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessWifiState) == (int)Permission.Granted;
        }

        private bool CanChangeWifi()
        {
            return ContextCompat.CheckSelfPermission(Application.Context, Manifest.Permission.ChangeWifiState) == (int)Permission.Granted;
        }
    }
}