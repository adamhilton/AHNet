using System;
using System.Linq;
using System.Collections.Generic;
using AHNet.Web.Core.Entities;
using AHNet.Web.Core.Exceptions;
using AHNet.Web.Core.Extensions;

namespace AHNet.Web.Infrastructure.Data
{
    public class BlogPostRepository : AHNetRepository<BlogPost>

    {
        public BlogPostRepository(AHNetDbContext context)
            : base(context)
        {
        }

        public IEnumerable<BlogPost> Take(int count)
        {
            return _dbSet.Take(count).ToList();
        }

        public BlogPost GetByTitle(string blogPostTitle)
        {
            var blogPost = _dbSet.FirstOrDefault(
                w => string.Equals(
                    w.Title.RemoveSpecialCharacters(), 
                    blogPostTitle,
                    StringComparison.CurrentCultureIgnoreCase
                )
            );

            if (blogPost == null)
            {
                throw new BlogPostNotFoundException();
            }
            
            return blogPost;
        }
    }
}