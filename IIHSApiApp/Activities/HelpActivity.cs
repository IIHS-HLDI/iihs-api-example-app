using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace IIHSApiApp.Activities
{
    [Activity(Theme = "@style/IIHSApiAppTheme")]
    public class HelpActivity : Activity
    {
        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(MenuActivity));
            StartActivity(intent);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            //ActionBar.Title = "Help & Feedback";
            //ActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_close);

            if (toolbar != null)
            {
                SetActionBar(toolbar);
                ActionBar.SetDisplayHomeAsUpEnabled(true);
                ActionBar.SetHomeButtonEnabled(true);
            }

            SetContentView(Resource.Layout.HelpLayout);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.help_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Selected: " + item.TitleFormatted, ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
    }
}