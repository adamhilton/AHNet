using AHNet.Web.Core.Exceptions;
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
            var model = new BlogIndexViewModel();
          
            return View(model);
        }

        [HttpGet("/Blog/{blogPostTitle}")]
        public IActionResult BlogPost(string blogPostTitle)
        {
            var model = new BlogPostContentViewModel();
            try
            {
                var blogPost = _blogPostRepository.GetByTitle(blogPostTitle);
                model = _mapper.Map<BlogPostContentViewModel>(blogPost);
            }
            catch (BlogPostNotFoundException)
            {
                //TODO: Log error
                return NotFound();
            }

            return View(model);
        }
    }
}