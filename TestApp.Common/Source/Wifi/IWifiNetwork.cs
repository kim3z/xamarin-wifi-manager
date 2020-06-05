using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Common.Source.Wifi
{
    public interface IWifiNetwork
    {
        string Ssid { get; set; }

        string Bssid { get; set; }

        int Level { get; set; }
    }
}
