﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Recipizer.Adapters
{
    class BTDeviceAdapter : BaseAdapter<BluetoothDevice>
    {
        Activity context;
        List<BluetoothDevice> devices;
        //Dictionary<string, BluetoothDevice> devicesDict;

        public BTDeviceAdapter(Activity context, List<BluetoothDevice> devices/*Dictionary<string, BluetoothDevice> devicesDict*/)
            : base()
        {
            this.context = context;
            //this.devicesDict = devicesDict;
            this.devices = devices; //devicesDict.Values.ToList();
        }

        /*public override void NotifyDataSetChanged()
        {
            //devices.Clear();
            //devices = devicesDict.Values.ToList();
            base.NotifyDataSetChanged();
        }*/

        public override int Count
        {
            get { return devices.Count; }
        }

        public override BluetoothDevice this[int position]
        {
            get { return devices[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            string deviceName;
            string deviceAddress;

            if (view == null)
            {
                view = this.context.LayoutInflater.Inflate(Resource.Layout.item_device, parent, false);
            }

            if (devices[position].Name == null)
                deviceName = "Undefiend";
            else
                deviceName = devices[position].Name;

            deviceAddress = devices[position].Address;


            view.FindViewById<TextView>(Resource.Id.textViewDeviceName).Text =
                deviceName;

            view.FindViewById<TextView>(Resource.Id.textViewDeviceAddress).Text =
                deviceAddress;

            return view;
        }
    }
}