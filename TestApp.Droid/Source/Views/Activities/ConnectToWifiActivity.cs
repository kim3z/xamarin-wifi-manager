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
using TestApp.Common.Source.View.Models;
using TestApp.Droid.Source.Utilities;

namespace TestApp.Droid.Source.Views.Activities
{
    [Activity(Label = "ConnectToWifiActivity")]
    public class ConnectToWifiActivity : Activity
    {
        private ConnectToWifiViewModel _viewModel;

        private ConnectToWifiViewModel ViewModel => _viewModel ?? (_viewModel = ViewModelLocator.Locator.ConnectToWifiViewModel);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            System.Diagnostics.Debug.WriteLine("CONNECTING TO WIFI");
            ViewModel.GetWifiNetWorks();
        }
    }
}