using AHNet.Web.Core.Entities;
using AHNet.Web.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;

namespace AHNet.Web.Infrastructure.Data
{
    public class SeedData
    {
        private readonly AHNetDbContext _ctx;
        private readonly UserManager<User> _userManager;
        private readonly IConfigurationRoot _configuration;

        public SeedData(AHNetDbContext ctx, 
            UserManager<User> userManager,
            IConfigurationRoot configuration)
        {
            _ctx = ctx;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task InitializeAsync()
        {
            if (NoUsersExist())
            {
                await CreateAdminAsync();
            }
        }

        private bool NoUsersExist()
        {
            return !_ctx.Users?.Any() ?? true;
        }

        private async Task CreateAdminAsync()
        {
            var userName = _configuration?.GetSeedData("AHNetSeedAdminUserName");
            var password = _configuration?.GetSeedData("AHNetSeedAdminPassword");
         
            await CreateUserAsync(userName, password);
        }
        
        private async Task CreateUserAsync(string userName, string password)
        {
            await _userManager.CreateAsync(new User()
            {
                UserName = userName
            }, password);
            _ctx.SaveChanges();
        }
    }
}
