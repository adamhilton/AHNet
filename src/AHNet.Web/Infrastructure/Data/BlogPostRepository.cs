using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHNet.Web.Core.Entities;
using AHNet.Web.Core.Exceptions;
using AHNet.Web.Core.Extensions;
using AHNet.Web.Features.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Sakura.AspNetCore;

namespace AHNet.Web.Infrastructure.Data
{
    public class BlogPostRepository : AHNetRepository<BlogPost>

    {
        public BlogPostRepository(AHNetDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<BlogPost>> GetRecentPostsAsync()
        {
            return await _dbSet.Where(w => w.IsPublished)
                .OrderByDescending(o => o.DatePublished)
                .Take(5).ToListAsync();
        }

        public BlogPost GetByTitle(string blogPostTitle)
        {
            var blogPost = _dbSet.FirstOrDefault(
                w => string.Equals(
                    w.Title.RemoveSpecialCharacters(),
                    blogPostTitle.RemoveSpecialCharacters(),
                    StringComparison.CurrentCultureIgnoreCase
                )
            );

            if (blogPost == null)
            {
                throw new BlogPostNotFoundException();
            }

            return blogPost;
        }

        public IPagedList<BlogPost> ToPagedList(int pageNumber, int pageSize)
        {
            return _dbSet.OrderByDescending(o => o.DatePublished).ToPagedList(pageSize, pageNumber);
        }

        public IPagedList<BlogPostWithContentTagsViewModel> ToPagedListWithContentTags(int pageNumber, int pageSize)
        {
            return _dbSet
                .Include(i => i.BlogPostsContentTags)
                .OrderByDescending(o => o.DatePublished)
                .Select(post => new BlogPostWithContentTagsViewModel()
                {
                    BlogPost = post,
                    ContentTags =
                        _dbContext.BlogPostsContentTags
                        .Where(w => w.BlogPostId == post.Id)
                        .Select(s => s.ContentTag)
                        .ToList()
                })
                .ToPagedList(pageSize, pageNumber);
        }

        public IPagedList<BlogPostWithContentTagsViewModel> ToPagedListOfPublishedBlogPosts(BlogPostsRequestViewModel request)
        {
            return _dbSet
                .FilterByTag(request.tag, _dbContext)
                .Where(w => w.IsPublished)
                .Include(i => i.BlogPostsContentTags)
                .OrderByDescending(o => o.DatePublished)
                .Select(post => new BlogPostWithContentTagsViewModel()
                {
                    BlogPost = post,
                    ContentTags =
                        _dbContext.BlogPostsContentTags
                        .Where(w => w.BlogPostId == post.Id)
                        .Select(s => s.ContentTag)
                        .ToList()
                })
                .ToPagedList(request.pageSize, request.page);
        }

        public List<ContentTag> GetContentTagsByBlogPostTitle(string title)
        {
            return _dbContext.BlogPostsContentTags
                .Include(i => i.ContentTag)
                .Where(w => string.Equals(
                        w.BlogPost.Title, title, StringComparison.CurrentCulture
                    )
                ).Select(s => s.ContentTag)
                .ToList();
        }

        public void ClearContentTags(int id)
        {
            var tagsToRemove = _dbContext.BlogPostsContentTags.Where(w => w.BlogPostId.Equals(id));
            foreach (var tag in tagsToRemove)
            {
                _dbContext.BlogPostsContentTags.Remove(tag);
            }
            _dbContext.SaveChanges();
        }

        public bool BlogPostTitleAlreadyExists(string title)
        {
            return _dbSet.Any(w => 
                string.Equals(
                    w.Title.RemoveSpecialCharacters(),
                    title.RemoveSpecialCharacters(),
                    StringComparison.CurrentCultureIgnoreCase
                )
            );
        }
    }
}