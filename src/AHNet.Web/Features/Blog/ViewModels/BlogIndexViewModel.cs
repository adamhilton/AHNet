
using System.Collections.Generic;
using AHNet.Web.Core.Entities;
using Sakura.AspNetCore;

namespace AHNet.Web.Features.Blog.ViewModels
{
    public class BlogIndexViewModel
    {
        public IPagedList<BlogPost> BlogPosts { get; set; }

        public BlogIndexViewModel()
        {
        }
    }
}