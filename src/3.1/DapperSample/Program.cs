using Dapper;
using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace DapperSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = Startup.Configuration(args);
            var connString = config.GetConnectionString("NorthwindDatabase");
            DapperQueryAndExecute(connString);
            DapperQueryAnonymous(connString);
            DapperStoredProcedure(connString);
            Console.Read();
        }

        /// <summary>
        /// Query Anonymous(dynamic type)
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static void DapperQueryAnonymous(string connString)
        {
            string sqlProducts = "SELECT * FROM Products;";
            using (var connection = new SqlConnection(connString))
            {
                var products = connection.Query(sqlProducts).FirstOrDefault();
                Console.WriteLine(products);
            }
        }

        /// <summary>
        /// Query by Stored Procedure
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static void DapperStoredProcedure(string connString)
        {
            string uspName = "CustOrdersDetail";

            using (var connection = new SqlConnection(connString))
            {
                var result = connection.Query<CustOrdersDetail>(
                    uspName, new {OrderId = 10248}, commandType: CommandType.StoredProcedure).ToList();

                Console.WriteLine(result.Count);
                result.ForEach(c =>
                    Console.WriteLine($"{c.ProductName},{c.UnitPrice},{c.Quantity},{c.Discount},{c.ExtendedPrice}"));
            }
        }

        /// <summary>
        /// Connection Northwind Database.
        /// Query for "SELECT" T-SQL.
        /// Execute for "UPDATE", "INSERT", "DELETE" T-SQL.
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static void DapperQueryAndExecute(string connString)
        {
            string sqlProducts = "SELECT * FROM Products;";
            string sqlOrderDetail = "SELECT * FROM [Order Details] WHERE OrderId = @OrderId;";
            string sqlOrderDetailInsert =
                "INSERT INTO [Order Details] ([OrderID],[ProductID],[UnitPrice],[Quantity],[Discount]) VALUES (@OrderID,@ProductID,@UnitPrice,@Quantity,@Discount);";
            string sqlOrderDetailDelete =
                "DELETE FROM [Order Details] WHERE [OrderID] = @OrderId AND [ProductID] = @ProductId;";

            using (var connection = new SqlConnection(connString))
            {
                var products = connection.Query<Products>(sqlProducts).ToList();
                var orderDetail = connection.Query<OrderDetail>(sqlOrderDetail, new {OrderId = 10248});
                var affectedRows = connection.Execute(sqlOrderDetailInsert, new
                {
                    OrderId = 10248,
                    ProductID = 1,
                    UnitPrice = 0.0,
                    Quantity = 1,
                    Discount = 0
                });
                var orderDetail2 = connection.Query<OrderDetail>(sqlOrderDetail, new {OrderId = 10248});

                // count 77
                Console.WriteLine(products.Count);
                // count 3
                Console.WriteLine(orderDetail.Count());
                // affectedRows 1
                Console.WriteLine(affectedRows);
                // count 3+1
                Console.WriteLine(orderDetail2.Count());

                affectedRows = connection.Execute(sqlOrderDetailDelete, new {OrderId = 10248, ProductId = 1});
                var orderDetail3 = connection.Query<OrderDetail>(sqlOrderDetail, new {OrderId = 10248});
                // affectedRows 1
                Console.WriteLine(affectedRows);
                // count 3+1-1
                Console.WriteLine(orderDetail3.Count());
            }
        }

        internal class CustOrdersDetail
        {
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public short Quantity { get; set; }
            public float Discount { get; set; }
            public decimal ExtendedPrice { get; set; }

        }

        internal class OrderDetail
        {
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public decimal UnitPrice { get; set; }
            public short Quantity { get; set; }
            public float Discount { get; set; }
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
