using System.Text;
using System.Text.RegularExpressions;
using AHNet.Web.Core.Entities;

namespace AHNet.Web.Core.Extensions
{
    public static class BlogPostExtensions
    {
        public static string GetBlogPostSummary(this BlogPost blogPost)
        {
            var MAXPARAGRAPHS = 2;
            var regex = new Regex("(<p[^>]*>.*?</p>)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var result = regex.Matches(blogPost.Body);
            StringBuilder bldr = new StringBuilder();
            var x = 0;
            foreach (Match m in result)
            {
                x++;
                bldr.Append(m.Value);
                if (x == MAXPARAGRAPHS) break;
            }
            return bldr.ToString();

        }
    }
}
