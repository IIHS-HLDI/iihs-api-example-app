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
    public class ClassesFragment : Fragment
    {
        private ApiService service;
        private List<Class> classes;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            service = new ApiService(ApiTokenConfig.ApiTokenKey);
        }

        public static ClassesFragment NewInstance()
        {
            var frag3 = new ClassesFragment { Arguments = new Bundle() };
            return frag3;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.ClassesLayout, container, false);
        }
        public async override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            if (this.View != null)
            {
                var buttons = this.View.FindViewById<LinearLayout>(Resource.Id.classesButtons);

                if (this.AssertConnected())
                {
                    try
                    {
                        this.classes = await ApiCache.Current.GetAsync<List<Class>>(ApiConfig.ClassesCacheKey);
                        if (this.classes == null)
                        {
                            this.classes = await service.GetClasses();
                            await ApiCache.Current.SetAsync(ApiConfig.ClassesCacheKey, this.classes);
                        }

                        foreach (var result in classes)
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
                    catch (Exception e)
                    {
                        DialogHelper.ShowErrorMesage(this.Context, e);
                    }
                }
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var slug = (string)button.Tag;
            var intent = new Intent(this.Context, typeof(MakeModelActivity));
            intent.PutExtra("slugClass", slug);
            var mySelectedClass = this.classes.Where(w => w.slug == slug).FirstOrDefault();
            intent.PutExtra("classTitle", $"{mySelectedClass.name}");
            Toast.MakeText(this.Context, $"Selected: {mySelectedClass.name}", ToastLength.Short).Show();
            StartActivity(intent);
        }
    }
}