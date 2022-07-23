namespace Basket.API.Configuration
{
    public class ApplicationSettings
    {
        public CacheSettings CacheSettings { get; set; }
        public GrpcSettings GrpcSettings { get; set; }
    }

    public class CacheSettings
    {
        public string ConnectionString { get; set; }
    }

    public class GrpcSettings
    {
        public string DiscountUrl { get; set; }
    }
}
