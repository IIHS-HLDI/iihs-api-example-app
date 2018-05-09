using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IIHSApiApp.Services;
using IIHSApiApp.Models;
using IIHSApiApp.Framework;
using Android.Views;

namespace IIHSApiApp.Activities
{
    [Activity(Theme = "@style/IIHSApiAppTheme")]
    public class SeriesRatingsActivity : BaseActivity
    {
        private ApiService service;
        private string makeSlug;
        private string makeTitle;
        private string year;

        Spinner yearSelectButton;
        Spinner seriesSelectButton;

        private List<Series> seriesList;
        private List<Year> yearsList;

        private ExpandableListViewAdapter mAdapter;
        private ExpandableListView expandableListView;
        //private int lastExpandedPosition = -1;
        private List<RatingGroupHeader> group = new List<RatingGroupHeader>();
        private Dictionary<RatingGroupHeader, List<Java.Lang.Object>> expandList = new Dictionary<RatingGroupHeader, List<Java.Lang.Object>>();

        public string seriesSlug { get; private set; }

        protected override int LayoutResource => Resource.Layout.SeriesRatingsLayout;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            service = new ApiService(ApiTokenConfig.ApiTokenKey);

            base.OnCreate(savedInstanceState);

            this.makeSlug = Intent.GetStringExtra("makeSlug");
            this.makeTitle = Intent.GetStringExtra("makeTitle");
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            this.SupportActionBar.SetHomeButtonEnabled(true);
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            this.SupportActionBar.Title = $"{this.makeTitle}";
            this.SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_close);

            expandableListView = FindViewById<ExpandableListView>(Resource.Id.expandableListView);

            if (this.AssertConnected())
            {
                try
                {
                    yearSelectButton = FindViewById<Spinner>(Resource.Id.yearSelectButton);
                    seriesSelectButton = FindViewById<Spinner>(Resource.Id.seriesSelectButton);
                    seriesSelectButton.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(seriesSelectButton_ItemSelected);
                    yearSelectButton.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(yearSelectButton_ItemSelected);

                    await loadSeriesSpinner();
                    await loadYearSpinner();
                }
                catch (Exception e)
                {
                    DialogHelper.ShowErrorMesage(this, e);
                }
            }

            mAdapter = ExpandableListViewAdapter.CreateFromData(this, (await service.GetSeriesRatings(this.year, this.makeSlug, this.seriesSlug)));            
            expandableListView.SetAdapter(mAdapter);
        }
        private async Task loadSeriesSpinner()
        {
            this.seriesList = (await service.GetAllSeries(this.makeSlug)).Where(w => !w.slug.Contains("(")).ToList();
            this.seriesSlug = this.seriesList.First().slug;
            var seriesItems = this.seriesList.Select(s => s.name.ToString()).ToList();
            var seriesSelectAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, seriesItems);
            seriesSelectAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            seriesSelectButton.Adapter = seriesSelectAdapter;
        }
        private async void seriesSelectButton_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner seriesSelectButton = (Spinner)sender;

            this.seriesSlug = this.GetSeriesSlugFromSeriesName((string)seriesSelectButton.GetItemAtPosition(e.Position));
            await loadYearSpinner();
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

        private string GetSeriesSlugFromSeriesName(string seriesName)
        {
            return this.seriesList.Where(w => w.name == seriesName).First().slug;
        }

        private async void yearSelectButton_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner yearSelectButton = (Spinner)sender;

            this.year = (string)yearSelectButton.GetItemAtPosition(e.Position);

            mAdapter = ExpandableListViewAdapter.CreateFromData(this, (await service.GetSeriesRatings(this.year, this.makeSlug, this.seriesSlug)));
            expandableListView.SetAdapter(mAdapter);
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