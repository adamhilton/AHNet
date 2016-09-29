
using AHNet.Web.Core.Entities;

namespace AHNet.Web.Infrastructure.Data
{
    public class ContentTagRepository : AHNetRepository<ContentTag>
    {
        public ContentTagRepository(AHNetDbContext context)
            : base(context)
        {
        }
    }
}
