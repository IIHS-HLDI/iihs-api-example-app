using System.Collections.Generic;

namespace IIHSApiApp.Models
{
    public class TopSafetyPick
    {
        public bool isTopSafetyPickPlus { get; set; }
        public int tspYear { get; set; }
        public bool isQualified { get; set; }
        public object builtAfter { get; set; }
        public string qualifyingText { get; set; }
        public string criteria { get; set; }
        public string lastModified { get; set; }
    }

    public class Photo
    {
        public string id { get; set; }
        public string url { get; set; }
        public string caption { get; set; }
        public string lastModified { get; set; }
    }

    public class Video
    {
        public string lastModified { get; set; }
        public string title { get; set; }
        public string playerUrl { get; set; }
        public object streamUrl { get; set; }
        public string downloadUrl { get; set; }
        public string contentType { get; set; }
        public string contentLength { get; set; }
        public string duration { get; set; }
        public string frameWidth { get; set; }
        public string frameHeight { get; set; }
    }

    public class SupportingTestFront
    {
        public string id { get; set; }
        public bool driverInjuryDataApplies { get; set; }
        public bool passengerInjuryDataApplies { get; set; }
        public bool intrusionDataApplies { get; set; }
        public string testedVin { get; set; }
        public string testedVehicleText { get; set; }
        public string lastModified { get; set; }
    }

    public class FrontalRatingsModerateOverlap
    {
        public int id { get; set; }
        public string iihsUrl { get; set; }
        public bool isPrimary { get; set; }
        public bool isQualified { get; set; }
        public object qualifyingText { get; set; }
        public object builtAfter { get; set; }
        public object builtBefore { get; set; }
        public string overallRating { get; set; }
        public string headNeckRating { get; set; }
        public string chestRating { get; set; }
        public string leftLegRating { get; set; }
        public string rightLegRating { get; set; }
        public string kinematicsRating { get; set; }
        public string structureRating { get; set; }
        public string testSubject { get; set; }
        public string lastModified { get; set; }
        public List<Photo> photos { get; set; }
        public List<Video> videos { get; set; }
        public List<SupportingTestFront> supportingTests { get; set; }
    }

    public class BuiltAfter
    {
        public int month { get; set; }
        public int year { get; set; }
    }

    public class SupportingTestSmallOverlap
    {
        public string id { get; set; }
        public bool driverInjuryDataApplies { get; set; }
        public bool passengerInjuryDataApplies { get; set; }
        public bool intrusionDataApplies { get; set; }
        public string testedVin { get; set; }
        public string testedVehicleText { get; set; }
        public string lastModified { get; set; }
    }

    public class FrontalRatingsSmallOverlap
    {
        public int id { get; set; }
        public string iihsUrl { get; set; }
        public bool isPrimary { get; set; }
        public bool isQualified { get; set; }
        public object qualifyingText { get; set; }
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

    public class SupportingTestSide
    {
        public string id { get; set; }
        public bool driverInjuryDataApplies { get; set; }
        public bool passengerInjuryDataApplies { get; set; }
        public bool intrusionDataApplies { get; set; }
        public string testedVin { get; set; }
        public string testedVehicleText { get; set; }
        public string lastModified { get; set; }
    }

    public class SideRating
    {
        public int id { get; set; }
        public string iihsUrl { get; set; }
        public bool isPrimary { get; set; }
        public bool isQualified { get; set; }
        public object qualifyingText { get; set; }
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

    public class RolloverRating
    {
        public int id { get; set; }
        public string iihsUrl { get; set; }
        public bool isPrimary { get; set; }
        public bool isQualified { get; set; }
        public object qualifyingText { get; set; }
        public object builtAfter { get; set; }
        public object builtBefore { get; set; }
        public string overallRating { get; set; }
        public string force { get; set; }
        public string weight { get; set; }
        public string ratio { get; set; }
        public string testSubject { get; set; }
        public object notes { get; set; }
        public string lastModified { get; set; }
        public List<object> photos { get; set; }
        public List<object> videos { get; set; }
    }

    public class RearRating
    {
        public int id { get; set; }
        public string iihsUrl { get; set; }
        public bool isPrimary { get; set; }
        public bool isQualified { get; set; }
        public object qualifyingText { get; set; }
        public BuiltAfter builtAfter { get; set; }
        public object builtBefore { get; set; }
        public string overallRating { get; set; }
        public string dynamicRating { get; set; }
        public string geometryRating { get; set; }
        public string seatType { get; set; }
        public object testSubject { get; set; }
        public string lastModified { get; set; }
        public List<object> photos { get; set; }
        public List<object> videos { get; set; }
    }

    public class OverallRating
    {
        public int totalPoints { get; set; }
        public string ratingText { get; set; }
    }

    public class Autobrake
    {
        public string availability { get; set; }
        public bool tested { get; set; }
        public string systemName { get; set; }
        public string packageName { get; set; }
        public int lowSpeedPoints { get; set; }
        public int highSpeedPoints { get; set; }
        public bool isAvailable { get; set; }
    }

    public class ForwardCollisionWarning
    {
        public string availability { get; set; }
        public string systemName { get; set; }
        public string packageName { get; set; }
        public int points { get; set; }
        public bool isAvailable { get; set; }
    }

    public class FrontCrashPreventionRating
    {
        public int id { get; set; }
        public string iihsUrl { get; set; }
        public bool isPrimary { get; set; }
        public bool isQualified { get; set; }
        public string qualifyingText { get; set; }
        public BuiltAfter builtAfter { get; set; }
        public object builtBefore { get; set; }
        public OverallRating overallRating { get; set; }
        public Autobrake autobrake { get; set; }
        public ForwardCollisionWarning forwardCollisionWarning { get; set; }
        public object testSubject { get; set; }
        public string lastModified { get; set; }
    }

    public class Trim
    {
        public string description { get; set; }
        public object optionalPackage { get; set; }
    }

    public class HeadlightRating
    {
        public int id { get; set; }
        public string iihsUrl { get; set; }
        public bool isPrimary { get; set; }
        public bool isQualified { get; set; }
        public object qualifyingText { get; set; }
        public object builtAfter { get; set; }
        public object builtBefore { get; set; }
        public string overallRating { get; set; }
        public bool highBeamAssist { get; set; }
        public bool curveAdaptive { get; set; }
        public string sourceHighBeamDescription { get; set; }
        public string sourceLowBeamDescription { get; set; }
        public string chartUrl { get; set; }
        public List<Trim> trims { get; set; }
        public string lastModified { get; set; }
    }

    public class SeriesRatingsData
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string iihsUrl { get; set; }
        public string modelYear { get; set; }
        public Make make { get; set; }
        public Class @class { get; set; }
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