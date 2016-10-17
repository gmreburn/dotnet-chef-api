namespace EBSCO.ChefServer.Models
{
    using System.Collections.Generic;

    public class CookBook
    {
        public string CookbookName { get; set; }
        public List<object> Files { get; set; }
        public string ChefType { get; set; }
        public List<object> Definitions { get; set; }
        public List<object> Libraries { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<object> Providers { get; set; }
        public List<object> Resources { get; set; }
        public List<Template> Templates { get; set; }
        public List<RootFile> RootFiles { get; set; }
        public string Name { get; set; }
        public bool Frozen { get; set; }
        public string Version { get; set; }
        public string JsonClass { get; set; }
        public Metadata Metadata { get; set; }
    }
}