namespace Catalog.API.Models
{
    internal class ApplicationSettings
    {
        public MongoSettings MongoDbSettings { get; set; }
    }

    internal class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
