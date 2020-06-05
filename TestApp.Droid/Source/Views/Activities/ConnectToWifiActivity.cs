using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using TestApp.Common.Source.View.Models;
using TestApp.Droid.Source.Adapter.Wifi;
using TestApp.Droid.Source.Utilities;
using TestApp.Droid.Source.Views.Adapters;

namespace TestApp.Droid.Source.Views.Activities
{
    [Activity(Label = "ConnectToWifiActivity")]
    public class ConnectToWifiActivity : Activity
    {
        private ConnectToWifiViewModel _viewModel;

        private ConnectToWifiViewModel ViewModel => _viewModel ?? (_viewModel = ViewModelLocator.Locator.ConnectToWifiViewModel);

        private List<Binding> _bindings;

        private WifiNetworkAdapter _adapter;

        private ListView _networksListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_connect_to_wifi);
            _adapter = new WifiNetworkAdapter(this, ViewModel.WifiNetworks);
            InitElements();
            InitBindings();
        }

        protected override void OnResume()
        {
            base.OnResume();
            ViewModel.ScanWifiNetworksContinuously();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ViewModel.StopScanningWifiNetworks();
        }

        protected override void OnPause()
        {
            base.OnPause();
            ViewModel.StopScanningWifiNetworks();
        }

        private void InitElements()
        {
            _networksListView = FindViewById<ListView>(Resource.Id.lvWifiNetworks);
            _networksListView.Adapter = _adapter;
        }

        private void InitBindings()
        {
            _bindings = new List<Binding>
            {
                this.SetBinding(() => ViewModel.WifiNetworks).WhenSourceChanges(OnWifiNetworksChanged),
            };
        }

        private void OnWifiNetworksChanged()
        {
            RunOnUiThread(() => {
                _adapter.NotifyDataSetChanged();
            });
        }

    }
}