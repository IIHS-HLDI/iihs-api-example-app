using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using System;

namespace IIHSApiApp.Fragments
{
    public class AppInfoFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public static AppInfoFragment NewInstance()
        {
            var frag5 = new AppInfoFragment { Arguments = new Bundle() };
            return frag5;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.AppInfoLayout, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            
            if (this.View != null)
            {
                TextView versionNumber = View.FindViewById<TextView>(Resource.Id.versionNumber);
                TextView buildNumber = View.FindViewById<TextView>(Resource.Id.buildNumber);

                versionNumber.Text = Context.PackageManager.GetPackageInfo(Context.PackageName, 0).VersionName;
                buildNumber.Text = $"{Context.PackageManager.GetPackageInfo(Context.PackageName, 0).VersionCode}";
            }
        }
    }
}