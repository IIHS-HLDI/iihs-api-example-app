namespace IIHSApiApp.Models
{
    public class ForwardCollisionWarning
    {
        public string availability { get; set; }
        public string systemName { get; set; }
        public string packageName { get; set; }
        public int points { get; set; }
        public bool isAvailable { get; set; }
    }
}