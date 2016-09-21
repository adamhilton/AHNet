using AHNet.Web.Core.Entities;
using AHNet.Web.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AHNet.Web.Features.Admin
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IRepository<BlogPost> _blogPostRepository;

        public AdminController(IRepository<BlogPost> blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BlogPosts()
        {
            var posts = _blogPostRepository.List();
            return View(posts);
        }
    }
}
