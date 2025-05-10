using Dapper;
using Dapper_Test.DBContext;
using Dapper_Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dapper_Test.Repositories
{
    public class StateRepository
    {
        private readonly DapperContext _context;
        public StateRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<State>> GetAllAsync()
        {
            var query = "SELECT * FROM State WHERE IsDeleted = 0";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<State>(query);
        }

        public async Task<State?> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM State WHERE Id = @Id AND IsDeleted = 0";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<State>(query, new { Id = id });
        }

        public async Task<int> CreateAsync(State state)
        {
            var query = "INSERT INTO State (Name, IsDeleted) VALUES (@Name, 0)";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, state);
        }

        public async Task<int> UpdateAsync(State state)
        {
            var query = "UPDATE State SET Name = @Name WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, state);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "UPDATE State SET IsDeleted = 1 WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}