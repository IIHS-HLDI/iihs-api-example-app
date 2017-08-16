using System.Collections.Generic;

namespace IIHSApiApp.Models
{
    public class FrontalRatingsSmallOverlap
    {
        public int id { get; set; }
        public string iihsUrl { get; set; }
        public bool isPrimary { get; set; }
        public bool isQualified { get; set; }
        public string qualifyingText { get; set; }
        public BuiltAfter builtAfter { get; set; }
        public object builtBefore { get; set; }
        public string overallRating { get; set; }
        public string headNeckRating { get; set; }
        public string chestRating { get; set; }
        public string femurPelvisRating { get; set; }
        public string footTibiaRating { get; set; }
        public string kinematicsRating { get; set; }
        public string structureRating { get; set; }
        public string testSubject { get; set; }
        public string lastModified { get; set; }
        public List<Photo> photos { get; set; }
        public List<Video> videos { get; set; }
        public List<SupportingTestSmallOverlap> supportingTests { get; set; }
    }
}