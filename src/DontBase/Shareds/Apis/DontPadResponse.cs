namespace DontBase.Shareds.Apis
{
    public class DontPadResponse
    {
        public long lastModified { get; set; }
        public bool changed { get; set; }
        public string body { get; set; }
    }
}