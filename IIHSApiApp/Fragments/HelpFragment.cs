using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using IIHSApiApp.Activities;

namespace IIHSApiApp.Fragments
{
    public class HelpFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.RetainInstance = true;

            Intent intent = new Intent(this.Activity, typeof(HelpActivity));
            StartActivity(intent);
        }

        public static HelpFragment NewInstance()
        {
            var frag7 = new HelpFragment { Arguments = new Bundle() };
            return frag7;
        }
    }
}