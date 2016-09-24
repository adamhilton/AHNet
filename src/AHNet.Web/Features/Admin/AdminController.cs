using AHNet.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AHNet.Web.Features.Admin
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly BlogPostRepository _blogPostRepository;

        public AdminController(BlogPostRepository blogPostRepository)
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
            var posts = _blogPostRepository.Take(5);
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
