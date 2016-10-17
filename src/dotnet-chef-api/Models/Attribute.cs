namespace EBSCO.ChefServer.Models
{
    using System;

    public class Attribute
    {
        public Uri Url { get; set; }
        public string Path { get; set; }
        public string Specificity { get; set; }
        public string Name { get; set; }
        public string Checksum { get; set; }
    }
}