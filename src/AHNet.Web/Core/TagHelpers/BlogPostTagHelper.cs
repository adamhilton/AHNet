using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AHNet.Web.Core.TagHelpers
{
    [HtmlTargetElement("a", Attributes = BlogTitleAttributeName)]
    public class BlogPostTagHelper : TagHelper
    {
        private const string BlogTitleAttributeName = "ahnet-blog-title";
        private const string BlogControllerName = "blog";

        [HtmlAttributeName(BlogTitleAttributeName)]
        public string BlogTitle { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string content = $"/{BlogControllerName}/{BlogTitle}";

            output.Attributes.SetAttribute("href", content);

            base.Process(context, output);
        }
    }
}
