using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TestApp.Common.Source.Wifi;

namespace TestApp.Droid.Source.Adapter.Wifi
{
    class WifiNetwork : IWifiNetwork
    {
        public string Ssid { get; set; }

        public bool HiddenSSID { get; set; }

        public int NetworkId { get; set; }

        public string ProvideFriendlyName { get; set; }
    }
}