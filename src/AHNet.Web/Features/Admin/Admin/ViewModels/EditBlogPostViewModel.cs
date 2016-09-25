using System;
using System.ComponentModel.DataAnnotations;

namespace AHNet.Web.Features.Admin.Admin.ViewModels
{
    public class EditBlogPostViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        [Display(Name = "Date Published")]
        public DateTime DatePublished { get; set; }
    }
}
