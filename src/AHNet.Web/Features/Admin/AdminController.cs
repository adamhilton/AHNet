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
        public IActionResult BlogPosts()
        {
            var posts = _blogPostRepository.ToPagedList(1, 30);

            var model = posts.Select(post => _mapper.Map<BlogPostPreviewViewModel>(post)).ToList();

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

        [HttpGet]
        public IActionResult EditBlogPost(string name)
        {
            var blogPost = _blogPostRepository.GetByTitle(name);

            if (blogPost == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<EditBlogPostViewModel>(blogPost);

            return View(model);
        }

        [HttpPost]
        public IActionResult EditBlogPost(EditBlogPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var blogPost = _blogPostRepository.GetByTitle(model.Title);

                blogPost.Title = model.Title;
                blogPost.Body = model.Body;
                blogPost.DatePublished = model.DatePublished;

                _blogPostRepository.Update(blogPost);
            }

            return RedirectToAction("BlogPosts");
        }
    }
}
