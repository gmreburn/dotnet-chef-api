namespace EBSCO.ChefServer.Models
{
    using System.Collections.Generic;

    public class Role
    {
        public string Name { get; set; }
        public string ChefType { get; set; }
        public string JsonClass { get; set; }
        public List<Attribute> DefaultAttributes { get; set; }
        public string Description { get; set; }
        public List<string> RunList { get; set; }
        public List<Attribute> OverrideAttributes { get; set; }
    }
}