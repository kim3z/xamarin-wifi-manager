using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using TestApp.Common.Source.View.Models;
using TestApp.Droid.Source.Utilities;
using TestApp.Droid.Source.Views.Adapters;

namespace TestApp.Droid.Source.Views.Activities
{
    [Activity(Label = "ConnectToWifiActivity")]
    public class ConnectToWifiActivity : MainActivity
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

            SetActionBarTitle("Wifi Settings");

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