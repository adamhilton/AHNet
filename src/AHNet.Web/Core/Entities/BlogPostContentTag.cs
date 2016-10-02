namespace AHNet.Web.Core.Entities
{
    public class BlogPostContentTag
    {
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }

        public int ContentTagId { get; set; }
        public ContentTag ContentTag { get; set; }

    }
}
