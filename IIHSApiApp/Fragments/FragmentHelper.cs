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
using Android.Net;
using IIHSApiApp.Framework;

namespace IIHSApiApp.Fragments
{
    public static class FragmentHelper
    {
        public static bool AssertConnected(this Android.Support.V4.App.Fragment fragment)
        {
            ConnectivityManager conMgr = (ConnectivityManager)fragment.Context.GetSystemService(Context.ConnectivityService);
            bool connected = conMgr.ActiveNetworkInfo != null;

            if (!connected)
            {
                DialogHelper.ShowErrorMesage(fragment.Context, "You are not connected to the internet");
            }

            return connected;
        }
    }
}