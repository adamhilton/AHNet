using System.Linq;
using System.Collections.Generic;
using AHNet.Web.Core.Entities;

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

    }
}