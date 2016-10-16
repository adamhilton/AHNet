
namespace AHNet.Web.Infrastructure.Utilities
{
    public class PostgreSqlConnectionString
    {
        public string DatabaseHost { get; set; }
        public string DatabaseName { get; set; } 
        public string DatabaseOwner { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabasePort { get; set; } 
        public string DatabasePooling { get; set; }

        public override string ToString()
        {
            return $"User ID={DatabaseOwner};Password={DatabasePassword};Host={DatabaseHost};Port={DatabasePort};Database={DatabaseName};Pooling={DatabasePooling};";
        }
    }
}