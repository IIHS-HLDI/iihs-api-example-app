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
}