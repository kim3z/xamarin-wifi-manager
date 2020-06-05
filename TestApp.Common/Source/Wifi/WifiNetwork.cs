using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Common.Source.Wifi
{
    public class WifiNetwork : IWifiNetwork
    {
        public string Ssid { get; set; }

        public string Bssid { get; set; }

        public int Level { get; set; }
    }
}
