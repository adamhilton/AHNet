using Xunit;
using AHNet.Web.Controllers;
using Microsoft.AspNet.Mvc;

namespace AHNet.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexReturnViewResult()
        {
            var controller = new HomeController();
            var result = controller.Index();
            Assert.IsType(typeof(ViewResult), result);
        }
    }
}
