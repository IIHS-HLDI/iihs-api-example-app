using Android.Content;
using Android.Net;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using IIHSApiApp.Activities;
using IIHSApiApp.Services;
using IIHSApiApp.Models;
using IIHSApiApp.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace IIHSApiApp.Fragments
{
    public class MakesFragment : Fragment
    {
        private ApiService service;
        private List<Make> makes;
        private ApiCache cache;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            
            service = new ApiService(ApiConfig.ApiKey);
            cache = ApiCache.Current;
        }

        public static MakesFragment NewInstance()
        {
            var frag2 = new MakesFragment { Arguments = new Bundle() };
            return frag2;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.MakesLayout, container, false);
        }

        public async override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            if (this.View != null)
            {
                var buttons = this.View.FindViewById<LinearLayout>(Resource.Id.makesButtons);

                if (this.AssertConnected())                
                {
                    try
                    {
                        this.makes = await this.cache.GetAsync<List<Make>>(ApiConfig.MakesCacheKey);
                        if (makes == null)
                        {
                            this.makes = await service.GetMakes();
                            await this.cache.SetAsync(ApiConfig.MakesCacheKey, this.makes);
                        }

                        foreach (var result in makes)
                        {
                            var button = new Button(this.Context)
                            {
                                Text = result.name,
                                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                                                                                 ViewGroup.LayoutParams.WrapContent)
                            };
                            button.Click += Button_Click;
                            button.Tag = result.slug;
                            buttons.AddView(button);
                        }
                    }
                    catch (Exception exp)
                    {
                        DialogHelper.ShowErrorMesage(this.Context, exp);
                    }
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var slug = (string)button.Tag;
            var intent = new Intent(this.Context, typeof(SeriesRatingsActivity));
            intent.PutExtra("makeSlug", slug);
            var mySelectedMake = this.makes.Where(w => w.slug == slug).FirstOrDefault();
            intent.PutExtra("makeTitle", $"{mySelectedMake.name}");
            Toast.MakeText(this.Context, $"Selected: {mySelectedMake.name}", ToastLength.Short).Show();
            StartActivity(intent);       
        }
    }
}