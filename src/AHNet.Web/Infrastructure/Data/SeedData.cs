using AHNet.Web.Core.Entities;
using AHNet.Web.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

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

            if (NoPostsExist())
            {
                CreateBlogPosts();
            }   
        }

        private void CreateBlogPosts()
        {
            for(int i = 0; i < 10; i++)
            {
                var newPost = new BlogPost { Title = $"My first blog - Part {i}!", 
                    Body = "Humblebrag Time!TwitterShoreditch single-origin coffee sustainable paleo mixtape schlitz fanny pack bespoke viral snackwave, vaporware sartorial trust fund tumeric. Before they sold out af plaid, banjo godard pinterest literally small batch 90's kinfolk tousled whatever venmo brooklyn occupy. Venmo echo park raclette, subway tile helvetica irony normcore cred. Readymade austin stumptown paleo, everyday carry photo booth kogi coloring book. Hell of activated charcoal vinyl chia enamel pin pork belly shoreditch. Narwhal sustainable direct trade crucifix tumeric austin. Kogi shabby chic ethical celiac yr, kale chips small batch single-origin coffee post-ironic polaroid actually vegan glossier intelligentsia.Asymmetrical whatever food truck, schlitz cray austin iceland. Chillwave scenester normcore tousled, hot chicken YOLO put a bird on it twee drinking vinegar locavore fingerstache viral tumeric mustache. XOXO brunch lo-fi, sartorial austin thundercats distillery keffiyeh venmo narwhal. Street art forage aesthetic, chartreuse waistcoat health goth bespoke knausgaard pitchfork hot chicken authentic freegan twee art party. Direct trade semiotics tattooed, tbh helvetica master cleanse fingerstache small batch slow-carb organic fanny pack ramps cred franzen normcore. Small batch flexitarian gastropub forage meggings. Keytar meditation XOXO helvetica iceland.Meh neutra iceland blog, sriracha dreamcatcher hella. Ennui intelligentsia slow-carb, franzen pickled knausgaard you probably haven't heard of them tote bag migas cornhole plaid polaroid. Jean shorts man braid semiotics, pop-up williamsburg tilde snackwave kitsch sustainable coloring book. Lomo authentic kickstarter cred freegan. Hoodie hot chicken literally, echo park 3 wolf moon 8-bit meditation photo booth. Artisan cred offal, poke fanny pack asymmetrical four loko drinking vinegar taxidermy umami fam cronut. Kogi iPhone venmo vice mustache ennui, selfies meh food truck chillwave la croix actually try-hard salvia.Cornhole irony live-edge, mixtape neutra skateboard tattooed la croix XOXO. Sustainable franzen vinyl tacos ennui. Shabby chic fanny pack normcore, tacos seitan cred migas heirloom air plant flexitarian plaid 3 wolf moon. Squid franzen locavore try-hard. VHS etsy farm-to-table beard, banjo live-edge activated charcoal XOXO pickled pork belly stumptown edison bulb. Kombucha synth tattooed ramps humblebrag pour-over hexagon. Poutine twee actually vinyl.",
                     DatePublished = DateTime.UtcNow };
                _ctx.BlogPosts.Add(newPost);
                _ctx.SaveChanges();
            }      
        }

        private bool NoPostsExist()
        {
            return !_ctx.BlogPosts?.Any() ?? true;
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
