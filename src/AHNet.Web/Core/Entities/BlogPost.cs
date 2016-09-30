using System;
using System.Collections.Generic;

namespace AHNet.Web.Core.Entities
{
    public class BlogPost : BaseEntity
    {
        public string Body { get; set; }
        public DateTime DatePublished { get; set; }
        public string Title { get; set; }
        public bool IsPublished { get; set; }

        public ICollection<BlogPostContentTag> BlogPostsContentTags { get; set; }

    }
}