using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Recipizer.Activities;
using Recipizer.Models;
using Newtonsoft.Json;

namespace Recipizer.Presenters
{
    class ShareBluetoothPresenter : IPresenter
    {

        IRecipizerView view;

        public ShareBluetoothPresenter(IRecipizerView _view, int id, string type)
        {
            this.view = _view;

            switch (type)
            {
                case "recipe":
                    Recipe r = Constants.Conn.Get<Recipe>(id);
                    r.SetIngredients();

                    if (!Bluetooth.ShareList.Contains(r))
                        Bluetooth.ShareList.Add(r);

                    break;

                //TODO add more
                default:
                    break;
            }
        }

        public void Share_Click()
        {
            if (Bluetooth.IsConnected)
            {
                string JsonString = JsonConvert.SerializeObject(Bluetooth.ShareList);
                Bluetooth.Write(JsonString);
            }
            else
            {
                //TODO Show Error Dialog
            }
        }

        public void NavToDeviceList()
        {
            view.Navigate(Constants.CONN_REQUEST, new Intent());
        }

        public void Clear_Click()
        {
            Bluetooth.ShareList.Clear();
            view.UpdateView();
        }

        public void Add_Click()
        {
            view.FinishView(Result.Ok, new Intent());
        }

        public void Connect_Click()
        {
            if (Bluetooth.IsConnected)
                view.MakeDialog(Constants.CONN_REQUEST);
            else
                NavToDeviceList();
        }

        public void onActivityResult(int requestCode, Result resultCode, Intent data) { }

        public void onBackPressed() { }

        public void onCreate()
        {
            Bluetooth.presenter = this;
            view.UpdateView();
        }

        public void onDestroy() { }

        public void onPause() { }

        public void onResume()
        {
            view.UpdateView();
        }
    }
}