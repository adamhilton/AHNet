using System.Linq;
using AHNet.Web.Core.Entities;
using AHNet.Web.Features.Admin.Admin.ViewModels;
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
        private readonly ContentTagRepository _contentTagRepository;
        private readonly IMapper _mapper;

        public AdminController(
            IMapper mapper,
            BlogPostRepository blogPostRepository,
            ContentTagRepository contentTagRepository)
        {
            _mapper = mapper;
            _blogPostRepository = blogPostRepository;
            _contentTagRepository = contentTagRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BlogPosts(int? page)
        {
            const int pageSize = 25;
            var pageNumber = page ?? 1;

            var model = _blogPostRepository.ToPagedListWithContentTags(pageNumber, pageSize);

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateBlogPost()
        {
            var model = new CreateBlogPostViewModel
            {
                ContentTags = _contentTagRepository.List().Select(s => s.Name).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateBlogPost(CreateBlogPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var blogPost = _mapper.Map<BlogPost>(model);

                _blogPostRepository.Add(blogPost);

                foreach (var tag in model.ContentTags)
                {
                    var contentTag = _contentTagRepository.GetByName(tag);
                    var blogtag = new BlogPostContentTag
                    {
                        BlogPost = blogPost,
                        ContentTag = contentTag,
                        BlogPostId = blogPost.Id,
                        ContentTagId = contentTag.Id
                    };

                    blogPost.BlogPostsContentTags.Add(blogtag);
                }

                _blogPostRepository.Update(blogPost);

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
            model.OldTitle = model.Title;
            model.AvailableContentTags = _contentTagRepository.List().Select(s => s.Name).ToList();
            model.SelectedContentTags =
                _blogPostRepository.GetContentTagsByBlogPostTitle(model.Title).Select(s => s.Name).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult EditBlogPost(EditBlogPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var blogPost = _blogPostRepository.GetByTitle(model.OldTitle);

                blogPost.Title = model.Title;
                blogPost.Body = model.Body;
                blogPost.DatePublished = model.DatePublished;
                blogPost.IsPublished = model.IsPublished;

                _blogPostRepository.ClearContentTags(blogPost.Id);

                foreach (var tagName in model.AvailableContentTags)
                {
                    var contentTag = _contentTagRepository.GetByName(tagName);

                    var blogtag = new BlogPostContentTag
                    {
                        BlogPost = blogPost,
                        ContentTag = contentTag,
                        BlogPostId = blogPost.Id,
                        ContentTagId = contentTag.Id
                    };

                    blogPost.BlogPostsContentTags.Add(blogtag);
                }

                _blogPostRepository.Update(blogPost);
            }

            return RedirectToAction("BlogPosts");
        }

        public IActionResult DeleteBlogPost(string name)
        {
            var blogPost = _blogPostRepository.GetByTitle(name);

            if (blogPost == null)
            {
                return NotFound();
            }

            _blogPostRepository.Delete(blogPost);

            return RedirectToAction("BlogPosts");
        }

        public IActionResult ValidateBlogPostTitle(string title)
        {
            return Json(!_blogPostRepository.BlogPostTitleAlreadyExists(title) ?
                "true" : "This title is already taken");
        }

        public IActionResult ContentTags(int? page)
        {
            const int pageSize = 25;
            var pageNumber = page ?? 1;

            var model = _contentTagRepository.ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateContentTag()
        {
            var model = new CreateContentTagViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateContentTag(CreateContentTagViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contentTag = _mapper.Map<ContentTag>(model);
                _contentTagRepository.Add(contentTag);

                return RedirectToAction("ContentTags");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditContentTag(string name)
        {
            var tag = _contentTagRepository.GetByName(name);
            var model = _mapper.Map<EditContentTagViewModel>(tag);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditContentTag(EditContentTagViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tag = _contentTagRepository.GetById(model.Id);
                tag.Name = model.Name;

                _contentTagRepository.Update(tag);

                return RedirectToAction("ContentTags");
            }

            return View(model);
        }

        public IActionResult DeleteContentTag(string name)
        {
            var tag = _contentTagRepository.GetByName(name);

            if(tag == null)
            {
                return NotFound();
            }

            _contentTagRepository.Delete(tag);

            return RedirectToAction("ContentTags");
        }

        public IActionResult ValidateContentTagName(string name)
        {
            return Json(!_contentTagRepository.ContentTagNameAlreadyExists(name) ?
                "true" : "This name is already taken");
        }
    }
}
