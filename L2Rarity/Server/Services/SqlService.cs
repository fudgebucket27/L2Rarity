using Dapper;
using L2Rarity.Server.Models;
using L2Rarity.Shared;
using System.Data.SqlClient;

namespace L2Rarity.Server.Services
{
    public class SqlService : ISqlService
    {
        private IConfiguration Configuration;

        public SqlService(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public async Task<List<Listing1Hour>> GetListings(string _collectionId)
        {
            using (var db = new SqlConnection(Configuration.GetValue<string>("DbConnectionString")))
            {
                await db.OpenAsync();
                var parameters = new { collectionId = _collectionId };
                var result = await db
                    .QueryAsync<Listing1Hour>
                    ("select * from listings where collectionId = @collectionId", parameters);
                return result.ToList();
            }
        }
    }
}
