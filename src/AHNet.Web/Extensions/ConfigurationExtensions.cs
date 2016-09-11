using Microsoft.Extensions.Configuration;

namespace AHNet.Web.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetSeedData(this IConfigurationRoot configuration, string name)
        {
            var section = configuration?.GetSection("SeedData");
            return section?[name];
        }
    }
}
