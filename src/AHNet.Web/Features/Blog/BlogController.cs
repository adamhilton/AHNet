using AHNet.Web.Core.Entities;
using AHNet.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace AHNet.Web.Features.Blog
{
    public class BlogController : Controller 
    {
        private readonly BlogPostRepository _blogPostRepository;

        public BlogController(BlogPostRepository blogPostRepository) 
        {
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var blogPosts = _blogPostRepository.List();
            return View(blogPosts);
        }
    }
}