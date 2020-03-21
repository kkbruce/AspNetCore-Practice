using System.Collections.Generic;

namespace FormatterSample.Models
{
    public class Blog
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Post> Posts { get; set; }
    }
}
