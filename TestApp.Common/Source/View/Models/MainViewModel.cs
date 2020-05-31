using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Common.Source.View.Models
{
    public class MainViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private RelayCommand _connectToWifiClickedCommand;

        public MainViewModel()
        {
            //
        }

        public RelayCommand ConnectToWifiClickedCommand =>
            _connectToWifiClickedCommand ?? (_connectToWifiClickedCommand = new RelayCommand(ConnectToWifiClicked));

        private void ConnectToWifiClicked()
        {
            Console.WriteLine("Connect to wifi clicked");
        }
    }
}
