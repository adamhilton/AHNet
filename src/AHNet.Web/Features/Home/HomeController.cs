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

        public IActionResult Index(int? page)
        {
            const int pageSize = 20;
            var pageNumber = (page ?? 1);

            var result = _blogPostRepository.ToPagedListOfPublishedBlogPosts(pageNumber, pageSize);

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
