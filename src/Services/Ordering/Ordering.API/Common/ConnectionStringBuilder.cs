namespace Ordering.API.Common
{
    public static class ConnectionStringBuilder
    {
        public static string BuildSqlConnectionString(string server, string database, string userId, string password)
        {
            return $"Server={server};Database={database};User Id={userId}; Password={password}";
        }
    }
}
