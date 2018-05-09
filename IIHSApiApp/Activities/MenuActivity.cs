using Android.Content.PM;
using Android.App;
using Android.OS;
using Android.Views;
using IIHSApiApp.Fragments;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Widget;

namespace IIHSApiApp.Activities
{
    [Activity(Label = "@string/ApplicationName", Theme = "@style/IIHSApiAppTheme", LaunchMode = LaunchMode.SingleTop)]
    public class MenuActivity : BaseActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;

        public override void OnBackPressed()
        {
            drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
        }

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.nav_drawer;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Toast.MakeText(this, "Resources Downloaded", ToastLength.Short).Show();

            drawerLayout = this.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            //Set hamburger items menu
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            //setup navigation view
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            //handle navigation
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.ratingsInfo:
                        ListItemClicked(0);
                        break;
                    case Resource.Id.makeModel:
                        ListItemClicked(1);
                        break;
                    case Resource.Id.typeSize:
                        ListItemClicked(2);
                        break;
                    case Resource.Id.teens:
                        ListItemClicked(3);
                        break;
                    case Resource.Id.appInfo:
                        ListItemClicked(4);
                        break;
                    //case Resource.Id.appSettings:
                    //    ListItemClicked(5);
                    //    break;
                    //case Resource.Id.helpFeedback:
                    //    ListItemClicked(6);
                    //    break;
                }

                var title = e.MenuItem.TitleFormatted;

                //ActionBar.SetTitle(e.MenuItem.ItemId);
                SupportActionBar.TitleFormatted = title;
                drawerLayout.CloseDrawers();
            };

            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
                ListItemClicked(0);
            }
        }

        int oldPosition = -1;
        private void ListItemClicked(int position)
        {
            //this way we don't load twice, but you might want to modify this a bit.
            if (position == oldPosition)
                return;

            oldPosition = position;

            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    fragment = RatingsInfoFragment.NewInstance();
                    break;
                case 1:
                    fragment = MakesFragment.NewInstance();
                    break;
                case 2:
                    fragment = ClassesFragment.NewInstance();
                    break;
                case 3:
                    fragment = TeensFragment.NewInstance();
                    break;
                case 4:
                    fragment = AppInfoFragment.NewInstance();
                    break;
                //case 5:
                //    fragment = SettingsFragment.NewInstance();
                //    break;
                //case 6:
                //    fragment = HelpFragment.NewInstance();
                //    break;
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.action_about)
            {
                ListItemClicked(4);
            }
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}