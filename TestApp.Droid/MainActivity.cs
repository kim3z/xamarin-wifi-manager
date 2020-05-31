using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using TestApp.Common.Source.View.Models;
using TestApp.Droid.Source.Utilities;
using TestApp.Droid.Source.Views.Activities;
using Android.Content;

namespace TestApp.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button _connectToWifiBtn;
        private static MainViewModel ViewModel => ViewModelLocator.Locator.MainViewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            SetupElements();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void SetupElements()
        {
            // connectToWifiBtn
            _connectToWifiBtn = FindViewById<Button>(Resource.Id.connectToWifiBtn);
            _connectToWifiBtn.Click += WifiBtn_Clicked;
        }

        private void WifiBtn_Clicked(object sender, System.EventArgs e)
        {
            ViewModel.ConnectToWifiClickedCommand.Execute(null);
            var NxtAct = new Intent(this, typeof(ConnectToWifiActivity));
            StartActivity(NxtAct);
        }
    }
}