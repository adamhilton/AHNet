using AHNet.Web.Features.Shared.ViewModels;
using AHNet.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace AHNet.Web.Features.Home
{
    public class HomeController : Controller
    {
        private readonly BlogPostRepository _blogPostRepository;

        public HomeController(BlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public IActionResult Index(BlogPostsRequestViewModel request)
        {
            var result = _blogPostRepository.ToPagedListOfPublishedBlogPosts(request);

            var model = new HomeIndexViewModel()
            {
                BlogPosts = result
            };

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
