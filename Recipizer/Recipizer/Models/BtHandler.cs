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
    class BtHandler : Handler
    {
        public delegate void delAction(string text);
        private delAction OnWrite;
        private delAction OnRead;

        public BtHandler(delAction OnWrite, delAction OnRead)
        {
            this.OnWrite = OnWrite;
            this.OnRead = OnRead;
        }

        public override void HandleMessage(Message msg)
        {
            //base.HandleMessage(msg);

            switch (msg.What)
            {
                case Constants.MESSAGE_WRITE:
                    var wBuffer = (byte[])msg.Obj;
                    var wMessage = Encoding.ASCII.GetString(wBuffer);
                    OnWrite(wMessage);
                    break;

                case Constants.MESSAGE_READ:
                    var rBuffer = (byte[])msg.Obj;
                    var rMessage = Encoding.ASCII.GetString(rBuffer);
                    OnWrite(rMessage);
                    break;

                default:

                    break;
            }
        }
    }
}