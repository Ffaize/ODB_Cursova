using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace WebAPI.Helpers
{
    public class DbAccessService
    {
        private const string ConnectionString = "Data Source=localhost;Initial Catalog=ODB_Cursova;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;Packet Size=4096;Command Timeout=0";

        /// <summary>
        /// Gets all items from the database using a stored procedure.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public static async Task<List<TResult>> GetItems<TResult>(string procedureName)
        {
            await using var connection = new SqlConnection(ConnectionString);
            var result = await connection.QueryAsync<TResult>(procedureName, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        /// <summary>
        /// Adds a record to the database using a stored procedure.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="entity"></param>
        /// <returns></returns>

        public static async Task<int> AddRecord<TEntity>(string procedureName, TEntity entity)
        {
            await using var connection = new SqlConnection(ConnectionString);
            return await connection.ExecuteAsync(procedureName, entity, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Updates a record in the database using a stored procedure.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<int> UpdateRecord<TEntity>(string procedureName, TEntity entity)
        {
            await using var connection = new SqlConnection(ConnectionString);
            return await connection.ExecuteAsync(procedureName, entity, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Deletes a record from the database using a stored procedure.
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<int> DeleteRecord(string procedureName, Guid id)
        {
            await using var connection = new SqlConnection(ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            return await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }


        /// <summary>
        /// Gets a single item by ID from the database using a stored procedure.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<TResult?> GetItemById<TResult>(string procedureName, Guid id)
        {
            await using var connection = new SqlConnection(ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            return await connection.QuerySingleOrDefaultAsync<TResult>(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets all items by a specific parameter from the database using a stored procedure.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<List<TResult>> GetAllByParameter<TResult>(string procedureName, string parameterName, object value)
        {
            await using var connection = new SqlConnection(ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add(parameterName, value);

            var result = await connection.QueryAsync<TResult>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        /// <summary>
        /// Gets a single item by a specific parameter from the database using a stored procedure.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<TResult?> GetOneByParameter<TResult>(string procedureName, string parameterName, object value)
        {
            await using var connection = new SqlConnection(ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add(parameterName, value);

            return await connection.QuerySingleOrDefaultAsync<TResult>(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Executes a stored procedure with custom parameters and returns a single result.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<TResult?> ExecuteStoredProcedure<TResult>(string procedureName, Dictionary<string, object?> parameters)
        {
            try
            {
                await using var connection = new SqlConnection(ConnectionString);
                var dynamicParams = new DynamicParameters();
                
                foreach (var param in parameters)
                {
                    dynamicParams.Add($"@{param.Key}", param.Value);
                }

                return await connection.QuerySingleOrDefaultAsync<TResult>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException sqlEx)
            {
                // Log SQL errors for debugging
                throw new InvalidOperationException($"SQL error in stored procedure '{procedureName}': {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                // Re-throw other exceptions
                throw;
            }
        }
    }
}
