using System.Collections.Generic;

namespace IIHSApiApp.Models
{
    public class RearRating
    {
        public int id { get; set; }
        public string iihsUrl { get; set; }
        public bool isPrimary { get; set; }
        public bool isQualified { get; set; }
        public string qualifyingText { get; set; }
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
}