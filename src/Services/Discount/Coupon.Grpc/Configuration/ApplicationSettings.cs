namespace Coupon.Grpc.Configuration
{
    public class ApplicationSettings
    {
        public DatabaseSettings DbSettings { get; set; }
    }

    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
    }
}
