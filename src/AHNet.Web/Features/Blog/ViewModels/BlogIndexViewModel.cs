using AHNet.Web.Features.Shared.ViewModels;
using Sakura.AspNetCore;

namespace AHNet.Web.Features.Blog.ViewModels
{
    public class BlogIndexViewModel
    {
        public IPagedList<BlogPostWithContentTagsViewModel> BlogPosts { get; set; }
    }
}