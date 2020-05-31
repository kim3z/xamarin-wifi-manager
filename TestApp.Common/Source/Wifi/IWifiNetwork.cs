using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Common.Source.Wifi
{
    public interface IWifiNetwork
    {
        string Ssid { get; set; }

        bool HiddenSSID { get; set; }

        int NetworkId { get; set; }

        string ProvideFriendlyName { get; set; }
    }
}
