using System.Linq;
using System.Threading.Tasks;
using AHNet.Web.Core.Entities;
using AHNet.Web.Features.Admin.Admin.ViewModels;
using AHNet.Web.Features.Blog.ViewModels;
using AHNet.Web.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AHNet.Web.Features.Admin
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly BlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;

        public AdminController(
            IMapper mapper,
            BlogPostRepository blogPostRepository)
        {
            _mapper = mapper;
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BlogPosts()
        {
            var posts = await _blogPostRepository.TakeAsync(20);

            var model  = posts.Select(post => _mapper.Map<BlogPostPreviewViewModel>(post)).ToList();

            return View(model);
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
                var blogPost = _mapper.Map<BlogPost>(model);
                _blogPostRepository.Add(blogPost);

                return RedirectToAction("BlogPosts");
            }
            return View(model);
        }
    }
}
