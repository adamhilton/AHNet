using System;
using System.ComponentModel.DataAnnotations;
using AHNet.Web.Core.Extensions;

namespace AHNet.Web.Features.Blog.ViewModels
{
    public class BlogPostPreviewViewModel
    {
        public string Title { get; set; }
        public string UrlTitle => Title?.RemoveSpecialCharacters();
    
        [Display(Name = "Date Published")]
        public DateTime DatePublished { get; set; }
        
    }
}
