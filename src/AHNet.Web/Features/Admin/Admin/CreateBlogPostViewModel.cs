
using System;
using System.ComponentModel.DataAnnotations;

namespace AHNet.Web.Features.Admin
{
    public class CreateBlogPostViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        [Display(Name = "Date Published")]
        public DateTime DatePublished { get; set; }
    }
}