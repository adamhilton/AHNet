using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHNet.Web.Core.Entities;
using AHNet.Web.Core.Exceptions;
using AHNet.Web.Core.Extensions;
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

        public IPagedList<BlogPost> ToPagedListOfPublishedBlogPosts(int pageNumber, int pageSize)
        {
            return _dbSet
                .Where(w => w.IsPublished)
                .OrderByDescending(o => o.DatePublished)
                .ToPagedList(pageSize, pageNumber);
        }
    }
}