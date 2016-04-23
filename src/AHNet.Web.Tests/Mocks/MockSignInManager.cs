using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHNet.Entities;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using Moq;

namespace AHNet.Web.Tests.Mocks
{
    public class MockSignInManager : SignInManager<User>
    {
        public MockSignInManager(IHttpContextAccessor contextAccessor, UserManager<User> userManager)
            : base(userManager,
                  contextAccessor,
                  new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<ILogger<SignInManager<User>>>().Object)
        {
        }

        public override Task SignInAsync(User user, bool isPersistent, string authenticationMethod = null)
        {
            return Task.FromResult(0);
        }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return Task.FromResult(SignInResult.Success);
        }

        public override Task SignOutAsync()
        {
            return Task.FromResult(0);
        }
    }
}
