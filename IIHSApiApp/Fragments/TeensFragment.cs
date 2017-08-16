using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using System;

namespace IIHSApiApp.Fragments
{
    public class TeensFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public static TeensFragment NewInstance()
        {
            var frag4 = new TeensFragment { Arguments = new Bundle() };
            return frag4;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.TeensLayout, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Button IihsWebButton = this.View.FindViewById<Button>(Resource.Id.iihsWebsiteButton);

            if (this.View != null)
            {
                var webView = this.View.FindViewById<WebView>(Resource.Id.IIHSTeenYT);
                WebSettings settings = webView.Settings;
                settings.JavaScriptEnabled = true;
                webView.SetWebChromeClient(new WebChromeClient());
                webView.LoadUrl("https://www.youtube.com/embed/y9n4qp3ZuYE");

                IihsWebButton.Click += IihsWebButton_Click;
                void IihsWebButton_Click(object sender, EventArgs e)
                {
                    var uri = Android.Net.Uri.Parse("http://www.iihs.org/iihs/ratings/vehicles-for-teens");
                    var intent = new Intent(Intent.ActionView, uri);
                    StartActivity(intent);
                }
            }
        }
    }
}