
using Microsoft.AspNetCore.Mvc;

namespace AHNet.Web.Features.Blog
{
    public class BlogController : Controller 
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}