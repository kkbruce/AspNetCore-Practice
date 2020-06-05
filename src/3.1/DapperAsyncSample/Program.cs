using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DapperAsyncSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = Startup.Configuration(args);
            var connString = config.GetConnectionString("NorthwindDatabase");
            string sqlProducts = "SELECT * FROM Products;";
            using (var connection = new SqlConnection(connString))
            {
                var products = await connection.QueryAsync(sqlProducts).ConfigureAwait(true);
                Console.WriteLine($"Product counter: {products.AsList().Count}");
            }

            Console.Read();
        }
        internal class Products
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int? SupplierId { get; set; }
            public int? CategoryId { get; set; }
            public string QuantityPerUnit { get; set; }
            public decimal? UnitPrice { get; set; }
            public short? UnitsInStock { get; set; }
            public short? UnitsOnOrder { get; set; }
            public short? ReorderLevel { get; set; }
            public bool Discontinued { get; set; }
        }
    }
}
