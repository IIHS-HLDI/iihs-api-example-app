using System.Collections.Generic;

namespace IIHSApiApp.Models
{
    public class RolloverRating
    {
        public int id { get; set; }
        public string iihsUrl { get; set; }
        public bool isPrimary { get; set; }
        public bool isQualified { get; set; }
        public string qualifyingText { get; set; }
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
}