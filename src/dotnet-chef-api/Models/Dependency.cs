namespace EBSCO.ChefServer.Models
{
    using System.Collections.Generic;

    public class Dependency
    {
        public List<object> ruby { get; set; }
        public List<object> rubygems { get; set; }
    }
}