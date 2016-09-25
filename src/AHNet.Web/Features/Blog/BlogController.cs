using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Index(int? page)
        {
            var pageSize = 3;
            var pageNumber = (page ?? 1);

            List<BlogPostPreviewViewModel> posts;
            try
            {
                posts = _blogPostRepository.ToPagedList(pageNumber, pageSize)
                    .Select(post => _mapper.Map<BlogPostPreviewViewModel>(post))
                    .ToList();
            }
            catch (ArgumentOutOfRangeException)
            {
                return Redirect("/Error");
            }

            var model = new BlogIndexViewModel()
            {
                BlogPosts = posts
            };

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