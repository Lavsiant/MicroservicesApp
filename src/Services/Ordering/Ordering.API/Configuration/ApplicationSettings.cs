namespace Ordering.API.Configuration
{
    internal class ApplicationSettings
    {
        public MongoSettings MongoDbSettings { get; set; }
        public SQLEventStoreSettings SQLEventStoreSettings { get; set; }
        public EventBusSettings EventBusSettings { get; set; }
    }

    internal class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    internal class SQLEventStoreSettings
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    internal class EventBusSettings
    {
        public string HostAddress { get; set; }
    }
}
