namespace EBSCO.ChefServer.Models
{
    public class License
    {
        public bool LimitExceeded { get; set; }

        public int NodeLicense { get; set; }

        public int NodeCount { get; set; }

        public string UpgradeUrl { get; set; }
    }
}