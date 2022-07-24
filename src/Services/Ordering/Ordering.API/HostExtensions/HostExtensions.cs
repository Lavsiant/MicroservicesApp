using Microsoft.EntityFrameworkCore.Metadata;
using Ordering.API.Common;
using Ordering.API.Configuration;
using Ordering.Domain.Core.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Ordering.API.HostExtensions
{
    internal static class HostExtensions
    {
        public static IHost CreateDatabase(this IHost host, SQLEventStoreSettings settings)
        {
            var databaseNameToCreate = settings.Database.ToString();
            var masterDatabase = "master";
            try
            {
                using (var connection = new SqlConnection(ConnectionStringBuilder.BuildSqlConnectionString(settings.Server, masterDatabase, settings.UserId, settings.Password)))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = $"Create database {databaseNameToCreate}";

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return host;
        }

        public static IHost MigrateDatabase(this IHost host)
        {

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var connection = services.GetRequiredService<IDbConnection>();

                try
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"
                            CREATE TABLE [dbo].[EventStore](
                                [Id] [uniqueidentifier] NOT NULL,
                                [CreatedAt] [datetime2] NOT NULL,
                                [Sequence] [int] IDENTITY(1,1) NOT NULL,
                                [Version] [int] NOT NULL,
                                [EventTypeName] [nvarchar](250) NOT NULL,
                                [AggregateId] [nvarchar](250) NOT NULL,
                                [Data] [nvarchar](max) NOT NULL,
                            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";

                    command.ExecuteNonQuery();

                    command.CommandText = @"
                            CREATE UNIQUE NONCLUSTERED INDEX [ConcurrencyCheckIndex] ON [dbo].[EventStore]
                            ([Version] ASC, [AggregateId] ASC) WITH (
                                PAD_INDEX = OFF, 
                                STATISTICS_NORECOMPUTE = OFF, 
                                SORT_IN_TEMPDB = OFF, 
                                IGNORE_DUP_KEY = OFF, 
                                DROP_EXISTING = OFF, ONLINE = OFF, 
                                ALLOW_ROW_LOCKS = ON, 
                                ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                            GO";

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("lol");
                }
            }

            return host;
        }

        public static IHost InitializeEventBusConsumers(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var consumers = services.GetServices<IEventBusConsumer>();

                foreach (var consumer in consumers)
                {
                    consumer.InitializeSubscriber();
                }
            }

            return host;
        }
    }
}
