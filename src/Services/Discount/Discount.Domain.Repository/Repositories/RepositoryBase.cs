using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Domain.Repository.Repositories
{
    public class RepositoryBase
    {
        protected IDbConnection _connection;

        protected RepositoryBase(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }

        public async Task<T> QueryFirst<T>(string query, object parameters = null)
        {
            return await _connection.QueryFirstAsync<T>(query, parameters);               
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null)
        {          
            return await _connection.QueryFirstOrDefaultAsync<T>(query, parameters);
        }

        public async Task<T> QuerySingle<T>(string query, object parameters = null)
        {
            return await _connection.QuerySingleAsync<T>(query, parameters);
        }

        public async Task<T> QuerySingleOrDefault<T>(string query, object parameters = null)
        {
            return await _connection.QuerySingleOrDefaultAsync<T>(query, parameters);            
        }
    }
}
