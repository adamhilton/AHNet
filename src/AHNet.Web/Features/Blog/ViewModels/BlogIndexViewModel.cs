
using System.Collections.Generic;

namespace AHNet.Web.Features.Blog.ViewModels
{
    public class BlogIndexViewModel
    {
        public IList<BlogPostPreviewViewModel> BlogPosts { get; set; }

        public BlogIndexViewModel()
        {
            BlogPosts = new List<BlogPostPreviewViewModel>();
        }
    }
}