namespace EBSCO.ChefServer.Models
{
    public class Recipe
    {
        public string Url { get; set; }
        public string Path { get; set; }
        public string Specificity { get; set; }
        public string Name { get; set; }
        public string Checksum { get; set; }
    }
}