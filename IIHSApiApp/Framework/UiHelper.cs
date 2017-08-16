using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IIHSApiApp.Models;

namespace IIHSApiApp.Framework
{
    public static class UiHelper
    {
        public static int GetResourceGamp(string ratingText)
        {
            if (ratingText == "Good")
            {
                return Resource.Drawable.g;
            }
            else if (ratingText == "Acceptable")
            {
                return Resource.Drawable.a;
            }
            else if (ratingText == "Marginal")
            {
                return Resource.Drawable.m;
            }
            else if (ratingText == "Poor")
            {
                return Resource.Drawable.p;
            }
            else
            {
                return Resource.Drawable.nr;
            }

        }

        public static int GetResourceAeb(int? points)
        {
            switch (points)
            {
                case 0:
                    return Resource.Drawable.ca_0;
                case 1:
                    return Resource.Drawable.ca_1;
                case 2:
                    return Resource.Drawable.ca_2;
                case 3:
                    return Resource.Drawable.ca_3;
                case 4:
                    return Resource.Drawable.ca_4;
                case 5:
                    return Resource.Drawable.ca_5;
                case 6:
                    return Resource.Drawable.ca_6;
                default:
                    return Resource.Drawable.ca_0;                    
            }
        }
    }
}