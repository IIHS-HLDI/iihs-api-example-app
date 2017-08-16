using System.Collections.Generic;

namespace IIHSApiApp.Models
{
    public class SideRating
    {
        public int id { get; set; }
        public string iihsUrl { get; set; }
        public bool isPrimary { get; set; }
        public bool isQualified { get; set; }
        public string qualifyingText { get; set; }
        public object builtAfter { get; set; }
        public object builtBefore { get; set; }
        public string sideAirbagConfiguration { get; set; }
        public string sideAirbagDescription { get; set; }
        public string overallRating { get; set; }
        public string driverHeadProtectionRating { get; set; }
        public string driverHeadNeckRating { get; set; }
        public string driverTorsoRating { get; set; }
        public string driverPelvisLegRating { get; set; }
        public string passengerHeadProtectionRating { get; set; }
        public string passengerHeadNeckRating { get; set; }
        public string passengerTorsoRating { get; set; }
        public string passengerPelvisLegRating { get; set; }
        public string structureRating { get; set; }
        public string testSubject { get; set; }
        public string lastModified { get; set; }
        public List<Photo> photos { get; set; }
        public List<Video> videos { get; set; }
        public List<SupportingTestSide> supportingTests { get; set; }
    }
}