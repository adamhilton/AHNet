using Xunit;
using AHNet.Web.Controllers;
using Microsoft.AspNet.Mvc;

namespace AHNet.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        public HomeController Sut => new HomeController();

        [Fact]
        public void IndexReturnViewResult()
        {
            var result = Sut.Index();
            Assert.IsType(typeof(ViewResult), result);
        }
    }
}
