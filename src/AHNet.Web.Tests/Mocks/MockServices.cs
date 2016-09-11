using AHNet.Web.Entities;
using AHNet.Web.Tests.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace AHNet.Web.Tests.Mocks
{
    public class MockServices
    {
        public readonly UserManager<User> UserManager;
        public readonly SignInManager<User> SignInManager;

        public MockServices()
        {
            var services = new ServiceCollection();
            services.AddEntityFramework()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<MockAHNetDbContext>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MockAHNetDbContext>();

            var context = new DefaultHttpContext();
            context.Features.Set<IHttpAuthenticationFeature>(new HttpAuthenticationFeature());
            services.AddSingleton<IHttpContextAccessor>(h => new HttpContextAccessor
            {
                HttpContext = context
            });

            var serviceProvider = services.BuildServiceProvider();
            UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
        }
    }
}
