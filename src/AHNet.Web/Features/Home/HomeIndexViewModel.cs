using AHNet.Web.Core.Entities;
using Sakura.AspNetCore;

namespace AHNet.Web.Features.Home
{
    public class HomeIndexViewModel
    {
        public IPagedList<BlogPost> BlogPosts { get; set; }
    }
}
