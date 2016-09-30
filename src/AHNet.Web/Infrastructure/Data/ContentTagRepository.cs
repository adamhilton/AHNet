
using System;
using System.Collections.Generic;
using System.Linq;
using AHNet.Web.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Sakura.AspNetCore;

namespace AHNet.Web.Infrastructure.Data
{
    public class ContentTagRepository : AHNetRepository<ContentTag>
    {
        public ContentTagRepository(AHNetDbContext context)
            : base(context)
        {
        }

        public IPagedList<ContentTag> ToPagedList(int pageNumber, int pageSize)
        {
            return _dbSet.OrderByDescending(o => o.Name).ToPagedList(pageSize, pageNumber);
        }

        public List<BlogPost> GetBlogPostsByContentTagName(string name)
        {
            return _dbContext.BlogPostsContentTags.Include(i => i.BlogPost)
                .Where(w => string.Equals(w.ContentTag.Name, name, StringComparison.CurrentCultureIgnoreCase))
                .Select(s => s.BlogPost)
                .ToList();
        }
    }
}
