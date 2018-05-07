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

namespace Recipizer.Activities
{
    public interface IRecipizerView
    {
        void UpdateView();
        void FinishView(Result result);
        void MakeToast(string text, ToastLength length);

        void Navigate();
    }
}