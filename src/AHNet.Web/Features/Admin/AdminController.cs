using AHNet.Web.Core.Entities;
using AHNet.Web.Core.Interfaces;
using AHNet.Web.Features.Admin;
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

        [HttpGet]
        public IActionResult BlogPosts()
        {
            var posts = _blogPostRepository.List();
            return View(posts);
        }

        [HttpGet]
        public IActionResult CreateBlogPost()
        {
            var model = new CreateBlogPostViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateBlogPost(CreateBlogPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("BlogPosts");
            }
            return View(model);
        }
    }
}
