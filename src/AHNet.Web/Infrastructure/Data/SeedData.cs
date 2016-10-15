using AHNet.Web.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;
using System;

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

        public async Task DevelopInitializeAsync()
        {
            await InitializeAsync();

            if (NoPostsExist())
            {
                CreateBlogPosts();
            }
        }

        private void CreateBlogPosts()
        {
            var sampleText = string.Empty;
            for (var j = 0; j < 5; j++)
            {
                sampleText +=
                    $"<p>Cold-pressed pok pok salvia, enamel pin next level vape trust fund. Pabst williamsburg live-edge brunch selfies, 3 wolf moon tofu synth. Tumblr VHS messenger bag, flannel intelligentsia try-hard cornhole forage four loko succulents whatever. Humblebrag seitan godard semiotics, four loko heirloom asymmetrical. Paleo woke lo-fi neutra. Yuccie mumblecore polaroid banh mi gentrify subway tile, small batch truffaut listicle cronut roof party wolf pabst mlkshk tumblr. Jean shorts etsy paleo tacos slow-carb, blue bottle try-hard whatever coloring book man bun. Selvage selfies subway tile tacos narwhal try-hard hoodie, bitters fam enamel pin forage lyft activated charcoal tofu. Subway tile +1 waistcoat, listicle cronut narwhal gochujang sriracha schlitz knausgaard austin cornhole.</p> {getSampleCode()} <p>Hammock green juice copper mug yr. Chia tbh fingerstache skateboard meggings tumeric. Affogato butcher succulents, art party synth retro waistcoat literally kogi twee</p>";
            }

            for (var i = 0; i < 40; i++)
            {
                var newPost = new BlogPost
                {
                    Title = $"My first blog - Part {i}!",
                    Body = sampleText,
                    DatePublished = DateTime.UtcNow,
                    IsPublished = true
                };
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
            var userName = _configuration.GetValue<string>("AHNET_ADMINUSER") ?? string.Empty;
            var password = _configuration.GetValue<string>("AHNET_ADMINPASS") ?? string.Empty;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Seed admin username or password is not set. Please set in enivronment or configuration file.");
            }

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

        private static string getSampleCode()
        {
            return "<pre class=\" language-csharp\"><code class=\" language-csharp\"><span class=\"token comment\" spellcheck=\"true\">// Declare the generic class.</span>\r\n<span class=\"token keyword\">public</span> <span class=\"token keyword\">class</span> <span class=\"token class-name\">GenericList</span><span class=\"token operator\">&lt;</span>T<span class=\"token operator\">&gt;</span>\r\n<span class=\"token punctuation\">{</span>\r\n    <span class=\"token keyword\">void</span> <span class=\"token function\">Add</span><span class=\"token punctuation\">(</span>T input<span class=\"token punctuation\">)</span> <span class=\"token punctuation\">{</span> <span class=\"token punctuation\">}</span>\r\n<span class=\"token punctuation\">}</span>\r\n<span class=\"token keyword\">class</span> <span class=\"token class-name\">TestGenericList</span>\r\n<span class=\"token punctuation\">{</span>\r\n    <span class=\"token keyword\">private</span> <span class=\"token keyword\">class</span> <span class=\"token class-name\">ExampleClass</span> <span class=\"token punctuation\">{</span> <span class=\"token punctuation\">}</span>\r\n    <span class=\"token keyword\">static</span> <span class=\"token keyword\">void</span> <span class=\"token function\">Main</span><span class=\"token punctuation\">(</span><span class=\"token punctuation\">)</span>\r\n    <span class=\"token punctuation\">{</span>\r\n        <span class=\"token comment\" spellcheck=\"true\">// Declare a list of type int.</span>\r\n        GenericList<span class=\"token operator\">&lt;</span><span class=\"token keyword\">int</span><span class=\"token operator\">&gt;</span> list1 <span class=\"token operator\">=</span> <span class=\"token keyword\">new</span> <span class=\"token class-name\">GenericList</span><span class=\"token operator\">&lt;</span><span class=\"token keyword\">int</span><span class=\"token operator\">&gt;</span><span class=\"token punctuation\">(</span><span class=\"token punctuation\">)</span><span class=\"token punctuation\">;</span>\r\n\r\n        <span class=\"token comment\" spellcheck=\"true\">// Declare a list of type string.</span>\r\n        GenericList<span class=\"token operator\">&lt;</span><span class=\"token keyword\">string</span><span class=\"token operator\">&gt;</span> list2 <span class=\"token operator\">=</span> <span class=\"token keyword\">new</span> <span class=\"token class-name\">GenericList</span><span class=\"token operator\">&lt;</span><span class=\"token keyword\">string</span><span class=\"token operator\">&gt;</span><span class=\"token punctuation\">(</span><span class=\"token punctuation\">)</span><span class=\"token punctuation\">;</span>\r\n\r\n        <span class=\"token comment\" spellcheck=\"true\">// Declare a list of type ExampleClass.</span>\r\n        GenericList<span class=\"token operator\">&lt;</span>ExampleClass<span class=\"token operator\">&gt;</span> list3 <span class=\"token operator\">=</span> <span class=\"token keyword\">new</span> <span class=\"token class-name\">GenericList</span><span class=\"token operator\">&lt;</span>ExampleClass<span class=\"token operator\">&gt;</span><span class=\"token punctuation\">(</span><span class=\"token punctuation\">)</span><span class=\"token punctuation\">;</span>\r\n    <span class=\"token punctuation\">}</span>\r\n<span class=\"token punctuation\">}</span></code></pre>";
        }
    }
}
