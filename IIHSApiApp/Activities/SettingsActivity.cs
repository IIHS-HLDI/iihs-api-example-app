using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace IIHSApiApp.Activities
{
    [Activity(Theme = "@style/IIHSApiAppTheme")]
    public class SettingsActivity : Activity
    {
        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(MenuActivity));
            StartActivity(intent);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //ActionBar.Title = "Settings";
            //ActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_arrow_back);

            SetContentView(Resource.Layout.SettingsLayout);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.settings_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Selected: " + item.TitleFormatted, ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
    }
}