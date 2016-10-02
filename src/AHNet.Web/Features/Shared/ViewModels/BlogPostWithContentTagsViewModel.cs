using System.Collections.Generic;
using AHNet.Web.Core.Entities;

namespace AHNet.Web.Features.Shared.ViewModels
{
    public class BlogPostWithContentTagsViewModel
    {
        public BlogPost BlogPost { get; set; }
        public ICollection<ContentTag> ContentTags { get; set; } = new List<ContentTag>();
    }
}
