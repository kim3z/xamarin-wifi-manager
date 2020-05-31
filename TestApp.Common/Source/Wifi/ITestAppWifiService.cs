using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Common.Source.Wifi
{
    public interface ITestAppWifiService
    {
        Task<List<IWifiNetwork>> GetWifiNetworks();
    }
}
