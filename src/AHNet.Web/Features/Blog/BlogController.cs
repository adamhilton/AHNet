using System;
using System.Collections.Generic;
using System.Linq;
using AHNet.Web.Core.Entities;
using AHNet.Web.Core.Exceptions;
using AHNet.Web.Features.Blog.ViewModels;
using AHNet.Web.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sakura.AspNetCore;

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
            var pageSize = 20;
            var pageNumber = (page ?? 1);

            var blogPosts = _blogPostRepository.ToPagedList(pageNumber, pageSize);

            var model = new BlogIndexViewModel()
            {
                BlogPosts = blogPosts
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