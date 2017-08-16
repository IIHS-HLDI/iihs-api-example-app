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

namespace IIHSApiApp.Activities
{
    public static class ActivityHelper
    {
        public static bool AssertConnected(this Activity activity)
        {
            ConnectivityManager conMgr = (ConnectivityManager)activity.GetSystemService(Context.ConnectivityService);
            bool connected = conMgr.ActiveNetworkInfo != null;

            if (!connected)
            {
                DialogHelper.ShowErrorMesage(activity, "You are not connected to the internet");
            }

            return connected;
        }
    }
}