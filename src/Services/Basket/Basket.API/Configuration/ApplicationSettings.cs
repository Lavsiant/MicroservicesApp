namespace Basket.API.Configuration
{
    public class ApplicationSettings
    {
        public CacheSettings CacheSettings { get; set; }
        public GrpcSettings GrpcSettings { get; set; }
        public EventBusSettings EventBusSettings { get; set; }
    }

    public class CacheSettings
    {
        public string ConnectionString { get; set; }
    }

    public class GrpcSettings
    {
        public string DiscountUrl { get; set; }
    }

    public class EventBusSettings
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
