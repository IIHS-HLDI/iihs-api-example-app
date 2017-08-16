using System.Collections.Generic;

namespace IIHSApiApp.Models
{

    public class SeriesRatingsData
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string iihsUrl { get; set; }
        public string modelYear { get; set; }
        public Make make { get; set; }
        public Class vehicleClass { get; set; }
        public TopSafetyPick topSafetyPick { get; set; }
        public List<FrontalRatingsModerateOverlap> frontalRatingsModerateOverlap { get; set; }
        public List<FrontalRatingsSmallOverlap> frontalRatingsSmallOverlap { get; set; }
        public List<SideRating> sideRatings { get; set; }
        public List<RolloverRating> rolloverRatings { get; set; }
        public List<RearRating> rearRatings { get; set; }
        public List<FrontCrashPreventionRating> frontCrashPreventionRatings { get; set; }
        public List<HeadlightRating> headlightRatings { get; set; }
    }
}