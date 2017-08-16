using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Net;
using IIHSApiApp.Models;
using IIHSApiApp.Services;
using IIHSApiApp.Framework;
using System.Collections.Generic;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace IIHSApiApp.Activities
{
    [Activity(Theme = "@style/IIHSApiAppTheme")]
    public class MakeModelActivity : BaseActivity
    {
        private ApiService service;
        private List<MakesModels> makesModels;
        private string slugClass;
        private string classTitle;

        protected override int LayoutResource => Resource.Layout.MakeModelLayout;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            service = new ApiService(ApiConfig.ApiKey);

            base.OnCreate(savedInstanceState);

            this.slugClass = Intent.GetStringExtra("slugClass");
            this.classTitle = Intent.GetStringExtra("classTitle");
            var buttons = this.FindViewById<LinearLayout>(Resource.Id.makesModelsButtons);

            this.SupportActionBar.SetHomeButtonEnabled(true);
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            this.SupportActionBar.Title = $"{this.classTitle}";

            if (this.AssertConnected())
            {
                try
                {
                    var progress = DialogHelper.ShowDownloadingMessage(this);
                    this.makesModels = (await service.GetMakesModels(this.slugClass)).Where(w => !w.series.slug.Contains("(")).ToList();

                    foreach (var result in makesModels)
                    {
                        var button = new Button(this)
                        {
                            Text = result.make.name + " " + result.series.name,
                            LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                                                                             ViewGroup.LayoutParams.WrapContent)
                        };
                        button.Click += Button_Click;
                        button.Tag = JsonConvert.SerializeObject(result);
                        buttons.AddView(button);
                    }
                    progress.Dismiss();
                }
                catch (Exception e)
                {
                    DialogHelper.ShowErrorMesage(this, e);
                }
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var mySelectedSeries = JsonConvert.DeserializeObject<MakesModels>((string)button.Tag);
            var intent = new Intent(this, typeof(ClassSeriesRatingsActivity));
            intent.PutExtra("makeTitle", $"{mySelectedSeries.make.name}");
            intent.PutExtra("makeSlug", $"{mySelectedSeries.make.slug}");
            intent.PutExtra("seriesTitle", $"{mySelectedSeries.series.name}");
            intent.PutExtra("seriesSlug", $"{mySelectedSeries.series.slug}");
            Toast.MakeText(this, $"Selected: {mySelectedSeries.make.name} {mySelectedSeries.series.name}", ToastLength.Short).Show();
            StartActivity(intent);
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