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

namespace TestApp.Droid.Source.Views.Adapters
{
    class WifiNetworkAdapter : BaseAdapter<IWifiNetwork>
    {

        Context context;
        private readonly IList<IWifiNetwork> wifiNetworks;

        public WifiNetworkAdapter(Context context, IList<IWifiNetwork> networks)
        {
            this.context = context;
            this.wifiNetworks = networks;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            WifiNetworkAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as WifiNetworkAdapterViewHolder;

            if (holder == null)
            {
                holder = new WifiNetworkAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.list_item_wifinetwork, parent, false);
                holder.Ssid = view.FindViewById<TextView>(Resource.Id.tvSSID);
                view.Tag = holder;
            }

            var level = this.wifiNetworks[position].Level;
            var connectionDescription = "No signal";

            if (level <= 0 && level >= -50)
            {
                connectionDescription = "Excellent";
            }
            else if (level < -50 && level >= -70)
            {
                connectionDescription = "Good";
            }
            else if (level < -70 && level >= -80)
            {
                connectionDescription = "Weak";
            }
            else if (level < -80 && level >= -100)
            {
                connectionDescription = "Very weak";
            }


            //fill in your items
            holder.Ssid.Text = $"Name: {this.wifiNetworks[position].Ssid}, Signal: {connectionDescription} ({level} dBm)";

            return view;
        }

        public override int Count => this.wifiNetworks.Count;

        public override IWifiNetwork this[int position] => throw new NotImplementedException();
    }

    class WifiNetworkAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
        public TextView Ssid { get; set; }
    }
}