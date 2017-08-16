namespace IIHSApiApp.Models
{
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
}