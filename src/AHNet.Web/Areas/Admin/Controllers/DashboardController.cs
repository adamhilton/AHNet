using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace AHNet.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
