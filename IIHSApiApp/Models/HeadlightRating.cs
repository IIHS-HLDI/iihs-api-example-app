using System.Collections.Generic;

namespace IIHSApiApp.Models
{
    public class HeadlightRating
    {
        public int id { get; set; }
        public string iihsUrl { get; set; }
        public bool isPrimary { get; set; }
        public bool isQualified { get; set; }
        public string qualifyingText { get; set; }
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
}