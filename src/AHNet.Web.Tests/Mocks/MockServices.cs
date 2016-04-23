using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHNet.Entities;
using AHNet.Web.Tests.Data;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features.Authentication;
using Microsoft.AspNet.Http.Features.Authentication.Internal;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Http.Features;

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
                .AddInMemoryDatabase()
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
