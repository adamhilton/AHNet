using System;

namespace AHNet.Web.Core.Exceptions
{
    public class BlogPostNotFoundException : Exception
    {
        public override string Message { get; } = "Blog post was not found";
    }
}
