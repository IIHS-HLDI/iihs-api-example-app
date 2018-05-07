using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.OS;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IIHSApiApp.Models;
using IIHSApiApp.Services;
using IIHSApiApp.Framework;

namespace IIHSApiApp.Activities
{
    [Activity(Label = "IIHS Api App", Theme = "@style/IIHSApiAppTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<Make> makes;
        private List<Class> classes;
        private ApiService service;
        

        public override void OnBackPressed()
        {
            /*
            this is blank preventing the user from closing the app by pressing
            the back button. I prefer this but I did just switch over to
            android from iOS.
            */
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            RequestedOrientation = ScreenOrientation.Portrait;

            service = new ApiService(ApiTokenConfig.ApiTokenKey);

            Button enterButton = FindViewById<Button>(Resource.Id.enterButton);
            enterButton.Click += EnterButton_Click;
            Go();
        }

        private void Go()
        {
            this.PreloadDataAndStartApp();
        }

        private void PreloadDataAndStartApp()
        {
            if (this.AssertConnected())
            {
                try
                {
                    DialogHelper.ShowDownloadingMessage(this);
                    
                    var task = new Task(async () => {
                        this.makes = await ApiCache.Current.GetAsync<List<Make>>(ApiConfig.MakesCacheKey);
                        if (this.makes == null)
                        {
                            this.makes = await service.GetMakes();
                            await ApiCache.Current.SetAsync(ApiConfig.MakesCacheKey, this.makes);
                        }

                        this.classes = await ApiCache.Current.GetAsync<List<Class>>(ApiConfig.ClassesCacheKey);
                        if (this.classes == null)
                        {
                            this.classes = await service.GetClasses();
                            await ApiCache.Current.SetAsync(ApiConfig.ClassesCacheKey, this.classes);
                        }
                        var intent = new Intent(this, typeof(MenuActivity));
                        StartActivity(intent);
                    });
                    task.Start();                    
                }
                catch (Exception e)
                {
                    DialogHelper.ShowErrorMesage(this, e);                    
                }
            }
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            if (this.AssertConnected())
            {
                try
                {
                    DialogHelper.ShowDownloadingMessage(this);

                    var task = new Task(async () => {
                        this.makes = await ApiCache.Current.GetAsync<List<Make>>(ApiConfig.MakesCacheKey);
                        if (this.makes == null)
                        {
                            this.makes = await service.GetMakes();
                            await ApiCache.Current.SetAsync(ApiConfig.MakesCacheKey, this.makes);
                        }

                        this.classes = await ApiCache.Current.GetAsync<List<Class>>(ApiConfig.ClassesCacheKey);
                        if (this.classes == null)
                        {
                            this.classes = await service.GetClasses();
                            await ApiCache.Current.SetAsync(ApiConfig.ClassesCacheKey, this.classes);
                        }

                        //if ((this.makes.Count + this.classes.Count) != 0)
                        //{
                        //    Toast.MakeText(this, "Resources Downloaded", ToastLength.Short).Show();
                        //}

                        var intent = new Intent(this, typeof(MenuActivity));
                        StartActivity(intent);
                    });
                    task.Start();
                }
                catch (Exception exp)
                {
                    DialogHelper.ShowErrorMesage(this, exp);
                }
            }
        }
    };
}