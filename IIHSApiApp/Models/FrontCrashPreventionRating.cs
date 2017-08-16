namespace IIHSApiApp.Models
{
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
}