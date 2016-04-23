using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AHNet.Entities;
using AHNet.Web.Areas.Admin.Controllers;
using AHNet.Web.Areas.Admin.ViewModels;
using AHNet.Web.Tests.Data;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Http.Features.Authentication;
using Microsoft.AspNet.Http.Features.Authentication.Internal;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using Xunit;

namespace AHNet.Web.Tests.Areas.Admin.Controllers
{
    public class AccountControllerTests
    {
        public AccountController Sut => new AccountController(_userManager, _signInManager);

        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountControllerTests()
        {
            var services = new ServiceCollection();
            services.AddEntityFramework()
                    .AddInMemoryDatabase()
                    .AddDbContext<MockAHNetDbContext>();

            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<MockAHNetDbContext>();

            var context = new DefaultHttpContext();
            context.Features.Set<IHttpAuthenticationFeature>(new HttpAuthenticationFeature());
            services.AddSingleton<IHttpContextAccessor>(h => new HttpContextAccessor { HttpContext = context });

            var serviceProvider = services.BuildServiceProvider();
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _signInManager = serviceProvider.GetRequiredService<SignInManager<User>>();
        }

        [Fact]
        public async void UsermanagerDoesNotCreateAUserWithABadPassword()
        {
            var user = new User()
            {
                UserName = "Foo"
            };
            
            await _userManager.CreateAsync(user, "Bar");
            var createdUser = _userManager.FindByNameAsync(user.UserName).Result;

            Assert.Null(createdUser);
        }
    }
}
