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

namespace IIHSApiApp.Models
{
    public class RatingGroupHeader
    {
        public string Text { get; set; }
        public int ImageResourceId { get; set; }
        public ETestTypes TestType { get; set; }
        public string RatingText { get; set; }
        public string QualifyingText { get; set; }
    }
}