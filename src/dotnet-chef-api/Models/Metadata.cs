namespace EBSCO.ChefServer.Models
{
    using System.Collections.Generic;

    public abstract class Metadata
    {
        public string Maintainer { get; set; }

        public List<Attribute> Attributes { get; set; }

        public Suggestions Suggestions { get; set; }

        public List<Recipe> Recipes { get; set; }

        public List<Dependency> Dependencies { get; set; }

        public List<Platform> Platforms { get; set; }

        public Groupings Groupings { get; set; }

        public Recommendations Recommendations { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public string MaintainerEmail { get; set; }

        public string LongDescription { get; set; }

        public Providing Providing { get; set; }

        public Replacing Replacing { get; set; }

        public Conflicting Conflicting { get; set; }

        public string License { get; set; }
    }
}