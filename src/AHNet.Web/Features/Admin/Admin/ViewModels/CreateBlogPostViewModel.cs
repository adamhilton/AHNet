using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AHNet.Web.Features.Admin.Admin.ViewModels
{
    public class CreateBlogPostViewModel
    {
        [Required]
        [Remote("ValidateBlogPostTitle", "Admin")]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        [Display(Name = "Date Published")]
        public DateTime DatePublished { get; set; } = DateTime.UtcNow;

        [Required]
        [Display(Name = "Published")]
        public bool IsPublished { get; set; } = false;

        [Display(Name = "Tags")]
        public List<string> ContentTags { get; set; } = new List<string>();

    }
}