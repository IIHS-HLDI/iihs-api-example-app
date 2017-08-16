using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Net;
using System.Threading.Tasks;
using IIHSApiApp.Services;
using IIHSApiApp.Models;
using IIHSApiApp.Framework;
using Android.Views;

namespace IIHSApiApp.Activities
{
    [Activity(Theme = "@style/IIHSApiAppTheme")]
    public class ClassSeriesRatingsActivity : BaseActivity
    {
        private ApiService service;
        private string makeTitle;
        private string makeSlug;
        private string seriesTitle;
        private string seriesSlug;
        private string year;
        private Spinner yearSelectButton;
        private List<Year> yearsList;
        
        private ExpandableListView expandableListView;

        protected override int LayoutResource => Resource.Layout.ClassSeriesRatingsLayout;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            service = new ApiService(ApiConfig.ApiKey);

            base.OnCreate(savedInstanceState);

            this.makeTitle = Intent.GetStringExtra("makeTitle");
            this.makeSlug = Intent.GetStringExtra("makeSlug");
            this.seriesTitle = Intent.GetStringExtra("seriesTitle");
            this.seriesSlug = Intent.GetStringExtra("seriesSlug");
            
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            this.SupportActionBar.SetHomeButtonEnabled(true);
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            this.SupportActionBar.Title = $"{this.makeTitle} {this.seriesTitle}";
            this.SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_close);

            expandableListView = FindViewById<ExpandableListView>(Resource.Id.expandableListView);            
            

            if (this.AssertConnected())
            {
                try
                {
                    yearSelectButton = FindViewById<Spinner>(Resource.Id.yearSelectButton);
                    yearSelectButton.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(yearSelectButton_ItemSelected);
                    
                    await loadYearSpinner();
                }
                catch (Exception e)
                {
                    DialogHelper.ShowErrorMesage(this, e);
                }
            }

            var data = await service.GetSeriesRatings(this.year, this.makeSlug, this.seriesSlug);
            expandableListView.SetAdapter(ExpandableListViewAdapter.CreateFromData(this, data));            
        }
        private async Task loadYearSpinner()
        {
            try
            {
                this.yearsList = await service.GetModelYearsForSeries(this.makeSlug, this.seriesSlug);
                var seriesYears = this.yearsList.Select(s => s.year.ToString()).OrderByDescending(o => o).ToList();
                this.year = seriesYears.First();
                var yearsSelectAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, seriesYears);
                yearsSelectAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                yearSelectButton.Adapter = yearsSelectAdapter;
            }
            catch (Exception e)
            {
                DialogHelper.ShowErrorMesage(this, e);
            }
        }

        private async void yearSelectButton_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner yearSelectButton = (Spinner)sender;

            this.year = (string)yearSelectButton.GetItemAtPosition(e.Position);

            var data = await service.GetSeriesRatings(this.year, this.makeSlug, this.seriesSlug);            
            expandableListView.SetAdapter(ExpandableListViewAdapter.CreateFromData(this, data));
        }      
        
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }

  
}