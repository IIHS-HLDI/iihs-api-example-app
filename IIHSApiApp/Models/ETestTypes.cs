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
    public enum ETestTypes
    {
        Frontal,
        Side,        
        RoofCrush,
        Rear,
        SmallOverlap,
        CrashAvoidance,            
        Unknown,
        Headlight
    }
}