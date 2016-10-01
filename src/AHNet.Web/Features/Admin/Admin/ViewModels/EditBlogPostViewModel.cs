using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AHNet.Web.Features.Admin.Admin.ViewModels
{
    public class EditBlogPostViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string OldTitle { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        [Display(Name = "Date Published")]
        public DateTime DatePublished { get; set; }

        [Required]
        public bool IsPublished { get; set; }

        public List<string> SelectedContentTags { get; set; } = new List<string>();

        [Display(Name = "Tags")]
        public List<string> AvailableContentTags { get; set; } = new List<string>();
    }
}
