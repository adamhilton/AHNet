
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace AHNet.Web.Features.Root
{
    [Route("")]
    public class RootController : Controller
    {

        [HttpGet("Error/{code:int}")]
        public IActionResult Error(int errorCode)
        {
        if (Response.StatusCode == (int)HttpStatusCode.NotFound ||
            errorCode == (int)HttpStatusCode.NotFound ||
            Request.Path.Value.EndsWith("404"))
        {
            return View("NotFound");
        }

        return View();
        }
    }
}