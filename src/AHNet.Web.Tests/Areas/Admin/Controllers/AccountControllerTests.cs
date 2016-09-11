using AHNet.Web.Areas.Admin.Controllers;
using AHNet.Web.Areas.Admin.ViewModels;
using AHNet.Web.Entities;
using AHNet.Web.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace AHNet.Web.Tests.Areas.Admin.Controllers
{
    public class AccountControllerTests
    {
        public AccountController Sut => new AccountController(
                                                _userManager,
                                                _signInManager);
        
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        public AccountControllerTests()
        {
            var context = new Mock<HttpContext>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            contextAccessor.Setup(x => x.HttpContext).Returns(context.Object);

            var services = new MockServices();

            _userManager = services.UserManager;
            _signInManager = new MockSignInManager(contextAccessor.Object, _userManager);
        }

        [Fact]
        public async void UsermanagerCanCreateUser()
        {
            var user = CreateMockUser();

            await _userManager.CreateAsync(user, "Bar123!");
            var createdUser = _userManager.FindByNameAsync(user.UserName).Result;

            Assert.NotNull(createdUser);
        }

        [Fact]
        public async void UsermanagerDoesNotCreateAUserWithABadPassword()
        {
            var user = CreateMockUser();

            await _userManager.CreateAsync(user, "Bar");
            var createdUser = _userManager.FindByNameAsync(user.UserName).Result;

            Assert.Null(createdUser);
        }

        [Fact]
        public void LogInPageReturnsAnActionResult()
        {
            var result = Sut.Login();

            Assert.NotNull(result);
        }

        [Fact]
        public async void UserCanLogInToAdminPortal()
        {
            var user = CreateMockUser();

            const string password = "Foo123!";

            await _userManager.CreateAsync(user, password);
            
            var result = await Sut.Login(new LoginViewModel()
            {
                Password = password,
                RememberMe = false,
                ReturnUrl = "",
                Username = user.UserName
            });
            
            Assert.Equal(0, Sut.ModelState.ErrorCount);
        }

        [Fact]
        public async void UserCanLogOutOfAdminPortal()
        {
            var result = await Sut.Logout();
          
            Assert.NotNull(result);
        }

        private static User CreateMockUser()
        {
            return new User()
            {
                UserName = "Foo"
            };
        }
    }
}