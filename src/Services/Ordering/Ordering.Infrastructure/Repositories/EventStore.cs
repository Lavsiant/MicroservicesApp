using Dapper;
using Newtonsoft.Json;
using Ordering.Infrastructure.Exceptions;
using Ordering.Infrastructure.Interfaces.EventStore;
using Ordering.Infrastructure.Models.EventStore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class EventStore : IEventStore
    {
        private const string EventStoreTableName = "EventStore";

        private const string EventStoreListOfColumns= $"[{nameof(Event.Id)}], [{nameof(Event.Data)}], [{nameof(Event.AggregateId)}], [{nameof(Event.Version)}], [{nameof(Event.CreatedAt)}], [{nameof(Event.EventTypeName)}]";
        private const string InsertParameters = $"@{nameof(Event.Id)}, @{nameof(Event.Data)}, @{nameof(Event.AggregateId)}, @{nameof(Event.Version)}, @{nameof(Event.CreatedAt)}, @{nameof(Event.EventTypeName)}";

        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore
        };


        private readonly IDbConnection _dbConnection;

        public EventStore(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AppendEventAsync(IDomainEvent @event)
        {
            try
            {
                var query =
                    $@"INSERT INTO {EventStoreTableName} ({EventStoreListOfColumns})
                    VALUES ({InsertParameters});";

                var eventDao = new Event()
                {
                    Id = @event.EventId,
                    Data = JsonConvert.SerializeObject(@event, Formatting.Indented, _jsonSerializerSettings),
                    AggregateId = @event.AggregateId,
                    EventTypeName = @event.GetType().Name,
                    Version = @event.AggregateVersion,
                    CreatedAt = DateTimeOffset.Now
                };

                await _dbConnection.ExecuteAsync(query, eventDao);
            }
            catch (Exception ex)
            {
                throw new EventStoreCommunicationException($"Error while appending event {@event.EventId} for aggregate {@event.AggregateId}", ex);
            }


        }

        public async Task<IEnumerable<IDomainEvent>> ReadEventsAsync(string id)
        {
            if (id is null)
            {
                throw new AggregateNotProvidedException("AggregateRootId cannot be null");
            }

            var query = new StringBuilder($@"SELECT {EventStoreListOfColumns} FROM {EventStoreTableName}");
            query.Append(" WHERE [AggregateId] = @AggregateId ");
            query.Append(" ORDER BY [Version] ASC;");


            var events = (await _dbConnection.QueryAsync<Event>(query.ToString(), new { AggregateId = id })).ToList();


            var domainEvents = events.Select(TransformEvent).Where(x => x != null).ToList().AsReadOnly();

            return domainEvents;
        }

        private IDomainEvent TransformEvent(Event eventSelected)
        {
            var obj = JsonConvert.DeserializeObject(eventSelected.Data, _jsonSerializerSettings);
            var evt = obj as IDomainEvent;

            return evt;
        }


    }
}
