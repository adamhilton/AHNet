using AHNet.Web.Features.Blog.ViewModels;
using AHNet.Web.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AHNet.Web.Features.Blog
{
    public class BlogController : Controller
    {
        private readonly IMapper _mapper;
        private readonly BlogPostRepository _blogPostRepository;

        public BlogController(
            IMapper mapper,
            BlogPostRepository blogPostRepository)
        {
            _mapper = mapper;
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var blogPosts = _blogPostRepository.Take(5);
            var model = new BlogIndexViewModel();

            foreach (var blogPost in blogPosts)
            {
                var mappedBlogPost = _mapper.Map<BlogPostPreviewViewModel>(blogPost);
                model.BlogPosts.Add(mappedBlogPost);
            }

            return View(model);
        }
    }
}