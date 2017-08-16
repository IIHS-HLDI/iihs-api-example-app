using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;

namespace IIHSApiApp.Fragments
{
    public class RatingsInfoFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public static RatingsInfoFragment NewInstance()
        {
            var frag1 = new RatingsInfoFragment { Arguments = new Bundle() };
            return frag1;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.RatingsInfoLayout, null);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Button IihsWebButton = this.View.FindViewById<Button>(Resource.Id.iihsWebsiteButton);

            if (this.View != null)
            {
                IihsWebButton.Click += IihsWebButton_Click;
                void IihsWebButton_Click(object sender, EventArgs e)
                {
                    var uri = Android.Net.Uri.Parse("http://www.iihs.org/iihs/ratings/ratings-info/frontal-crash-tests");
                    var intent = new Intent(Intent.ActionView, uri);
                    StartActivity(intent);
                }
            }
        }
    }
}