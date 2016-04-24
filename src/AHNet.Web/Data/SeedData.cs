using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHNet.Data;
using AHNet.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;

namespace AHNet.Web.Data
{
    public class SeedData
    {
        private readonly AHNetDbContext _ctx;
        private readonly UserManager<User> _userManager;
        private readonly IConfigurationRoot _configuration;

        public SeedData(AHNetDbContext ctx, UserManager<User> userManager, IConfigurationRoot configuration)
        {
            _ctx = ctx;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task InitializeAsync()
        {
            if (!_ctx.Users.Any())
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = _configuration["AHNetSeedUserName"]
                }, _configuration["AHNetSeedPassword"]);
                _ctx.SaveChanges();
            }
        }
    }
}
