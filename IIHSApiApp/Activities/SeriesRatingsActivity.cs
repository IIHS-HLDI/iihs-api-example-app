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
        private Dictionary<RatingGroupHeader, List<string>> expandList = new Dictionary<RatingGroupHeader, List<string>>();

        public string seriesSlug { get; private set; }

        protected override int LayoutResource => Resource.Layout.SeriesRatingsLayout;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            service = new ApiService(ApiConfig.ApiKey);

            base.OnCreate(savedInstanceState);

            this.makeSlug = Intent.GetStringExtra("makeSlug");
            this.makeTitle = Intent.GetStringExtra("makeTitle");
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            this.SupportActionBar.SetHomeButtonEnabled(true);
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            this.SupportActionBar.Title = $"{this.makeTitle}";
            this.SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_close);

            expandableListView = FindViewById<ExpandableListView>(Resource.Id.expandableListView);

            //expandableListView.SetOnGroupExpandListener(new OnGroupExpandListener()
            //{
            //    public override void onGroupExpand(int groupPosition)
            //{
            //    if (lastExpandedPosition != -1 && groupPosition != lastExpandedPosition)
            //    {
            //        expandableListView = groupPosition;
            //    }
            //    lastExpandedPosition = groupPosition;
            //}
            //});

            //expandableListView.ChildClick += (s, e) => {
            //    Toast.MakeText(this, "Clicked : " + mAdapter.GetChild(e.GroupPosition, e.ChildPosition), ToastLength.Short).Show();
            //};

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

            SetData(out mAdapter, (await service.GetClassSeriesRatings(this.year, this.makeSlug, this.seriesSlug)));
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

            this.SetData(out this.mAdapter, await service.GetSeriesRatings(this.year, this.makeSlug, this.seriesSlug));
            expandableListView.SetAdapter(mAdapter);
        }

        private void SetGamp(ImageView image, string gampText)
        {
            image.SetImageResource(UiHelper.GetResourceGamp(gampText));
        }

        private void ParseAndDisplay(List<SeriesRatingsData> seriesRatingsData)
        {
            ImageView gamp = FindViewById<ImageView>(Resource.Id.GAMP);
            //TextView frontCrash = FindViewById<TextView>(Resource.Id.frontCrashText);

            var firstSeries = seriesRatingsData.First();

            FrontalRatingsSmallOverlap ratingForPrimarySmallOverlapFront = firstSeries.frontalRatingsSmallOverlap.Where(w => w.isPrimary == true).FirstOrDefault();
            this.SetGamp(gamp, ratingForPrimarySmallOverlapFront?.overallRating);

            FrontalRatingsModerateOverlap ratingForPrimaryModerateOverlapFront = firstSeries.frontalRatingsModerateOverlap.Where(w => w.isPrimary == true).FirstOrDefault();
            this.SetGamp(gamp, ratingForPrimaryModerateOverlapFront?.overallRating);

            SideRating ratingForPrimarySide = firstSeries.sideRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            this.SetGamp(gamp, ratingForPrimarySide?.overallRating);

            RolloverRating ratingForPrimaryRoofStrength = firstSeries.rolloverRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            this.SetGamp(gamp, ratingForPrimaryRoofStrength?.overallRating);

            RearRating ratingForPrimaryHeadRestraint = firstSeries.rearRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            this.SetGamp(gamp, ratingForPrimaryHeadRestraint?.overallRating);

            //FrontCrashPreventionRating ratingForPrimaryFrontCrashPrevention = firstSeries.frontCrashPreventionRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            //if (ratingForPrimaryFrontCrashPrevention != null) { frontCrash.Text = $"{ratingForPrimaryFrontCrashPrevention.overallRating.ratingText}"; }
            //else { frontCrash.Text = "Not Rated"; }

            HeadlightRating ratingForPrimaryHeadlights = firstSeries.headlightRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            this.SetGamp(gamp, ratingForPrimaryHeadlights?.overallRating);
        }
        private void SetData(out ExpandableListViewAdapter mAdapter, List<SeriesRatingsData> seriesRatingsData)
        {
            group.Clear();
            expandList.Clear();

            List<string> smallOverlapFront = new List<string>
            {
                "Overall evaluation",
                "Structure and safety cage",
                "Injury measures",
                "Restraints and dummy kinematics"
            };
            List<string> moderateOverlapFront = new List<string>
            {
                "Overall evaluation",
                "Structure and safety cage",
                "Injury measures",
                "Restraints and dummy kinematics"
            };
            List<string> side = new List<string>
            {
                "Overall evaluation",
                "Structure and safety cage",
                "Injury measures",
                "Restraints and dummy kinematics"
            };
            List<string> roofStrength = new List<string>
            {
                "Overall evaluation",
                "Curb weight",
                "Peak force",
                "Strength-to-weight ratio",
                "Tested vehicle"
            };
            List<string> headRestraint = new List<string>
            {
                "Overall evaluation",
                "Dynamic rating",
                "Seat/head restraint geometry"
            };
            List<string> frontCrashPrevention = new List<string>
            {
                "Overall evaluation",
                "Forward collision warning",
                "Low-speed autobrake",
                "High-speed autobrake"
            };
            List<string> headlights = new List<string>
            {
                "Trim level(s)",
                "Low-beam headlight type",
                "High-beam headlight type",
                "Curve-adaptive?",
                "Automatically switches between low beams and high beams (high-beam assist)?",
                "Overall evaluation",
                "Distance at which headlights provide at least 5 lux illumination:",
                "Low beams",
                "High beams"
            };

            var firstSeries = seriesRatingsData.First();

            FrontalRatingsSmallOverlap ratingForPrimarySmallOverlapFront = firstSeries.frontalRatingsSmallOverlap.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { Text = "Small overlap front:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimarySmallOverlapFront?.overallRating) });

            FrontalRatingsModerateOverlap ratingForPrimaryModerateOverlapFront = firstSeries.frontalRatingsModerateOverlap.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { Text = "Moderate overlap front:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryModerateOverlapFront?.overallRating) });

            SideRating ratingForPrimarySide = firstSeries.sideRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { Text = "Side:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimarySide?.overallRating) });

            RolloverRating ratingForPrimaryRoofStrength = firstSeries.rolloverRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { Text = "Roof strength:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryRoofStrength?.overallRating) });

            RearRating ratingForPrimaryHeadRestraint = firstSeries.rearRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { Text = "Head restraints & seats:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryHeadRestraint?.overallRating) });

            FrontCrashPreventionRating ratingForPrimaryFrontCrashPrevention = firstSeries.frontCrashPreventionRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { Text = "Front crash prevention:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryFrontCrashPrevention?.overallRating.ratingText) });
            //  Yes this^ is returning the nr gamp every time ¯\_(ツ)_/¯ I'll fix it later


            HeadlightRating ratingForPrimaryHeadlights = firstSeries.headlightRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { Text = "Headlights:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryHeadlights?.overallRating) });

            expandList.Add(group[0], smallOverlapFront);
            expandList.Add(group[1], moderateOverlapFront);
            expandList.Add(group[2], side);
            expandList.Add(group[3], roofStrength);
            expandList.Add(group[4], headRestraint);
            expandList.Add(group[5], frontCrashPrevention);
            expandList.Add(group[6], headlights);

            mAdapter = new ExpandableListViewAdapter(this, group, expandList);
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