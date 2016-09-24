using System;
using System.ComponentModel.DataAnnotations;

namespace AHNet.Web.Features.Blog.ViewModels
{
    public class BlogPostPreviewViewModel
    {
        public string Title { get; set; }
        [Display(Name = "Date Published")]
        public DateTime DatePublished { get; set; }
    }
}
