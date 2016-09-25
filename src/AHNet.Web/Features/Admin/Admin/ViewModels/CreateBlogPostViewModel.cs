using System;
using System.ComponentModel.DataAnnotations;

namespace AHNet.Web.Features.Admin.Admin.ViewModels
{
    public class CreateBlogPostViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }

        [Display(Name = "Date Published")]
        public DateTime DatePublished { get; set; } = DateTime.UtcNow;
    }
}