
namespace AHNet.Web.Infrastructure.Utilities
{
    public class PostgreSqlConnectionString
    {
        public string DatabaseHost { get; set; } = "localhost";
        public string DatabaseName { get; set; } = string.Empty;
        public string DatabaseOwner { get; set; } = string.Empty;
        public string DatabasePassword { get; set; } = string.Empty;
        public string DatabasePort { get; set; } = "5432";
        public string DatabasePooling { get; set; } = "true";

        public string GetConnectionString()
        {
            return $"User ID={DatabaseOwner};Password={DatabasePassword};Host={DatabaseHost};Port={DatabasePort};Database={DatabaseName};Pooling={DatabasePooling};";
        }
    }
}