using AHNet.Web.Features.Shared.ViewModels;
using Sakura.AspNetCore;

namespace AHNet.Web.Features.Home
{
    public class HomeIndexViewModel
    {
        public IPagedList<BlogPostWithContentTagsViewModel> BlogPosts { get; set; }
    }
}
