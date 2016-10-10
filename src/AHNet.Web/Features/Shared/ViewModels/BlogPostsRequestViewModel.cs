
namespace AHNet.Web.Features.Shared.ViewModels
{
    public class BlogPostsRequestViewModel 
    {
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 20;
        public string tag { get; set; } = string.Empty;
        public string q { get; set; } = string.Empty;
    }
}