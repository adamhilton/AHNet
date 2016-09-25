using System.Collections.Generic;
using System.Threading.Tasks;
using AHNet.Web.Features.Blog.ViewModels;
using AHNet.Web.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AHNet.Web.Features.Shared.Components.RecentBlogPosts
{
    public class RecentBlogPosts : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly BlogPostRepository _blogPostRepository;

        public RecentBlogPosts(
            IMapper mapper,
            BlogPostRepository blogPostRepository)
        {
            _mapper = mapper;
            _blogPostRepository = blogPostRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogPosts = await _blogPostRepository.Take(5);
            var model = new List<BlogPostPreviewViewModel>();

            foreach (var blogPost in blogPosts)
            {
                var mappedBlogPost = _mapper.Map<BlogPostPreviewViewModel>(blogPost);
                model.Add(mappedBlogPost);
            }

            return View(model);
        }
    }
}
