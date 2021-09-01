using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly NpgsqlConnection _connection;

        public DiscountRepository(IConfiguration configuration)
        {
            _connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<bool> CreateAsync(Coupon coupon)
        {
            var sql = "insert into Coupon (ProductName, Description, Amount) values " +
                "(@ProductName, @Description, @Amount)";
            var rows = await _connection.ExecuteAsync(sql, coupon);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(string productName)
        {
            var sql = "delete from Coupon where ProductName=@ProductName";
            var rows = await _connection.ExecuteAsync(sql, new { ProductName = productName });
            return rows > 0;
        }

        public async Task<Coupon> GetAsync(string productName)
        {
            var sql = "select * from Coupon where ProductName=@ProductName";
            var coupon = await _connection.QueryFirstOrDefaultAsync<Coupon>(sql, new { ProductName = productName });
            return coupon ?? new Coupon { ProductName = "No Discount", Description = "No Discount Desc" };
        }

        public async Task<bool> UpdateAsync(Coupon coupon)
        {
            var sql = "update Coupon set ProductName=@ProductName, Description=@Description, Amount=@Amount " +
                "where Id=@Id";
            var rows = await _connection.ExecuteAsync(sql, coupon);
            return rows > 0;
        }
    }
}
