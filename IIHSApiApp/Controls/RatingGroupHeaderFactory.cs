using IIHSApiApp.Activities;
using IIHSApiApp.Models;

namespace IIHSApiApp.Controls
{
    public static class RatingGroupHeaderFactory
    {
        public static GroupControl<RatingGroupHeader> Create(ETestTypes testType)
        {
            switch (testType)
            {
                case ETestTypes.Frontal:                    
                case ETestTypes.Side:                    
                case ETestTypes.RoofCrush:
                case ETestTypes.Rear:
                case ETestTypes.SmallOverlap:
                case ETestTypes.Headlight:
                    return new GampGroupRatingHeader();
                case ETestTypes.CrashAvoidance:
                    return new AebGroupRatingHeader();
                case ETestTypes.Unknown:
                    return null;
                default:
                    return null;
            }
        }
    }

}