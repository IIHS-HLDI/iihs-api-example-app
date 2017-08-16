namespace IIHSApiApp.Models
{
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
}