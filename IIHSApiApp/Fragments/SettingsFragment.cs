using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using IIHSApiApp.Activities;

namespace IIHSApiApp.Fragments
{
    public class SettingsFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.RetainInstance = true;

            Intent intent = new Intent(this.Activity, typeof(SettingsActivity));
            StartActivity(intent);
        }
        public static SettingsFragment NewInstance()
        {
            var frag6 = new SettingsFragment { Arguments = new Bundle() };
            return frag6;
        }
    }
}