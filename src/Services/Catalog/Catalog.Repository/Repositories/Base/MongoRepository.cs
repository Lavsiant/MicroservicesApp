using Catalog.Models.Interfaces;
using Catalog.Repository.Exceptions;
using Catalog.Repository.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Repository.Repositories.Base
{
    public class MongoRepository<TDocument> : IRepository<TDocument> where TDocument : class, IMongoEntity
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoRepository(IMongoDatabase mongoDatabase, string collectionName)
        {
            _mongoDatabase = mongoDatabase;
            MongoCollection = _mongoDatabase.GetCollection<TDocument>(collectionName);
        }

        protected IMongoCollection<TDocument> MongoCollection { get; }

        public async Task<ICollection<TDocument>> FindAsync(Expression<Func<TDocument, bool>> predicate)
        {
            var cursor = await MongoCollection.FindAsync(predicate);
            return cursor.ToList();
        }

        public ICollection<TDocument> Find(Expression<Func<TDocument, bool>> predicate)
        {
            var cursor = MongoCollection.Find(predicate);
            return cursor.ToList();
        }

        public async Task<ICollection<TDocument>> GetAllAsync()
        {
            var cursor = await MongoCollection.FindAsync(_ => true);
            return cursor.ToList();
        }

        public async Task<TDocument> GetByIdAsync(string id)
        {
            return await MongoCollection
               .Find(x => x.Id == id)
               .SingleAsync();
        }

        public void Add(TDocument entity)
        {
            try
            {
                MongoCollection.InsertOne(entity);
            }
            catch (MongoWriteException ex)
            {
                throw new RepositoryException($"Error inserting entity {entity.Id}", ex);
            }
        }

        public async Task AddAsync(TDocument entity)
        {
            try
            {
                await MongoCollection.InsertOneAsync(entity);
            }
            catch (MongoWriteException ex)
            {
                throw new RepositoryException($"Error inserting entity {entity.Id}", ex);
            }
        }

        public void AddRange(IEnumerable<TDocument> entities)
        {
            try
            {
                MongoCollection.InsertMany(entities);
            }
            catch (MongoWriteException ex)
            {
                throw new RepositoryException($"Error inserting entities {String.Join(',', entities.Select((x) => x.Id).ToArray())}", ex);
            }
        }

        public async Task AddRangeAsync(IEnumerable<TDocument> entities)
        {
            try
            {
                await MongoCollection.InsertManyAsync(entities);
            }
            catch (MongoWriteException ex)
            {
                throw new RepositoryException($"Error inserting entities {String.Join(',', entities.Select((x) => x.Id).ToArray())}", ex);
            }
        }

        public void Remove(string id)
        {
            MongoCollection.DeleteOne(x => x.Id == id);
        }

        public async Task RemoveAsync(string id)
        {
            await MongoCollection.DeleteOneAsync(x => x.Id == id);
        }

        public void RemoveRange(IEnumerable<TDocument> entities)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<TDocument> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(TDocument entity)
        {
            try
            {
                var result = MongoCollection.ReplaceOne(x => x.Id == entity.Id, entity);

                if (result.MatchedCount != 1)
                {
                    throw new RepositoryException($"Missing entoty {entity.Id}");
                }
            }
            catch (MongoWriteException ex)
            {
                throw new RepositoryException($"Error updating entity {entity.Id}", ex);
            }
        }

        public async Task UpdateAsync(TDocument entity)
        {
            try
            {
                var result = await MongoCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

                if (result.MatchedCount != 1)
                {
                    throw new RepositoryException($"Missing entoty {entity.Id}");
                }
            }
            catch (MongoWriteException ex)
            {
                throw new RepositoryException($"Error updating entity {entity.Id}", ex);
            }
        }

    }
}
