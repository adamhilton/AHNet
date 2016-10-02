using System;
using System.Collections.Generic;
using AHNet.Web.Core.Entities;

namespace AHNet.Web.Features.Blog.ViewModels
{
    public class BlogPostContentViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DatePublished { get; set; }
        public ICollection<ContentTag> ContentTags { get; set; } = new List<ContentTag>();
    }
}
