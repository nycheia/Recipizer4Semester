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

namespace Recipizer.Models
{
    public class BtHandler : Handler
    {
        Presenters.DeviceListPresenter dlp;
        public BtHandler(Presenters.DeviceListPresenter dlp)
        {
            this.dlp = dlp;
        }

        public override void HandleMessage(Message msg)
        {
            switch (msg.What)
            {
                case Constants.MESSAGE_WRITE:
                    byte[] writeBuf = (byte[])msg.Obj;
                    string writeMessage = Encoding.ASCII.GetString(writeBuf);

                    dlp.hWrite(writeMessage);

                    // construct a string from the buffer
                    //string writeMessage = new string(writeBuf);
                    //Toast.makeText(this, "Write Message", Toast.LENGTH_SHORT).show();
                    break;
                case Constants.MESSAGE_READ:
                    byte[] readBuf = (byte[])msg.Obj;
                    // construct a string from the valid bytes in the buffer
                    string readMessage = Encoding.ASCII.GetString(readBuf);

                    dlp.hRead(readMessage);
                    //string readMessage = new string(readBuf, 0, msg.Arg1);
                    //Toast.makeText(this, "Read Message", Toast.LENGTH_SHORT).show();
                    break;
            }
        }
    }
}