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
            //DapperQueryAndExecute(connString);
            DapperQueryAnonymous(connString);
            DapperQueryStronglyTyped(connString);
            DapperQueryFirst(connString);
            DapperQueryMultiple(connString);
            DapperStoredProcedure(connString);
            Console.Read();
        }

        /// <summary>
        /// Execute multiple queries
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static void DapperQueryMultiple(string connString)
        {
            string sqlMultiple = "SELECT * FROM Products WHERE ProductID = @ProductId; SELECT * FROM [Order Details] WHERE ProductID = @ProductId;";
            using (var connection =  new SqlConnection(connString))
            {
                connection.Open();

                using (var queryMultiple = connection.QueryMultiple(sqlMultiple, new {ProductId = 1}))
                {
                    // Execute first SELECT
                    var products = queryMultiple.Read<Products>().First();
                    // Execute second SELECT
                    var orderDetails = queryMultiple.Read<OrderDetail>().ToList();

                    Console.WriteLine($"{nameof(DapperQueryMultiple)} first: {products.ProductName}");
                    Console.WriteLine($"{nameof(DapperQueryMultiple)} second: {orderDetails.Count}");
                }
            }
        }

        /// <summary>
        /// QueryFirst, QuerySingle, QueryFirstOrDefault, QuerySingleOrDefault
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static void DapperQueryFirst(string connString)
        {
            string sqlProducts = "SELECT * FROM Products WHERE ProductID = @ProductId;";
            using (var connection = new SqlConnection(connString))
            {
                // Query Anonymous
                var productAnonymous = connection.QueryFirst(sqlProducts, new { ProductId = 1 });
                Console.WriteLine($"{nameof(DapperQueryFirst)} Anonymous: {productAnonymous}");

                // Strongly Typed
                var productFirst = connection.QueryFirst<Products>(sqlProducts, new { ProductId = 1 });
                Console.WriteLine($"{nameof(DapperQueryFirst)} QueryFirst Id: {productFirst.ProductId}, Name: {productFirst.ProductName}");

                var productSingle = connection.QuerySingle<Products>(sqlProducts, new { ProductId = 1 });
                Console.WriteLine($"{nameof(DapperQueryFirst)} QuerySingle Id: {productSingle.ProductId}, Name: {productSingle.ProductName}");

                var productFirstOrDefault = connection.QueryFirstOrDefault<Products>(sqlProducts, new { ProductId = 1 });
                Console.WriteLine($"{nameof(DapperQueryFirst)} QueryFirstOrDefault Id: {productFirstOrDefault.ProductId}, Name: {productFirstOrDefault.ProductName}");

                var productSingleOrDefault = connection.QuerySingleOrDefault<Products>(sqlProducts, new { ProductId = 1 });
                Console.WriteLine($"{nameof(DapperQueryFirst)} SingleOrDefault Id: {productSingleOrDefault.ProductId}, Name: {productSingleOrDefault.ProductName}");
            }
        }

        /// <summary>
        /// Query by Strongly Typed
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static void DapperQueryStronglyTyped(string connString)
        {
            string sqlProducts = "SELECT * FROM Products;";
            using (var connection = new SqlConnection(connString))
            {
                var products = connection.Query<Products>(sqlProducts).ToList();
                Console.WriteLine($"{nameof(DapperQueryStronglyTyped)}: {products.Count}");
            }
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
                Console.WriteLine($"{nameof(DapperQueryAnonymous)}: {products}");
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
                    uspName, new { OrderId = 10248 }, commandType: CommandType.StoredProcedure).ToList();

                Console.WriteLine($"{nameof(DapperStoredProcedure)}: {result.Count}");
                result.ForEach(c =>
                    Console.WriteLine($"\t{nameof(DapperStoredProcedure)}: {c.ProductName},{c.UnitPrice},{c.Quantity},{c.Discount},{c.ExtendedPrice}"));
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
                var orderDetail = connection.Query<OrderDetail>(sqlOrderDetail, new { OrderId = 10248 });
                var affectedRows = connection.Execute(sqlOrderDetailInsert, new
                {
                    OrderId = 10248,
                    ProductID = 1,
                    UnitPrice = 0.0,
                    Quantity = 1,
                    Discount = 0
                });
                var orderDetail2 = connection.Query<OrderDetail>(sqlOrderDetail, new { OrderId = 10248 });

                // count 77
                Console.WriteLine($"{nameof(DapperQueryAndExecute)} sqlProducts: {products.Count}");
                // count 3
                Console.WriteLine($"{nameof(DapperQueryAndExecute)} sqlOrderDetail: {orderDetail.Count()}");
                // affectedRows 1
                Console.WriteLine($"{nameof(DapperQueryAndExecute)} sqlOrderDetailInsert: {affectedRows}");
                // count 3+1
                Console.WriteLine($"{nameof(DapperQueryAndExecute)} sqlOrderDetail: {orderDetail2.Count()}");

                affectedRows = connection.Execute(sqlOrderDetailDelete, new { OrderId = 10248, ProductId = 1 });
                var orderDetail3 = connection.Query<OrderDetail>(sqlOrderDetail, new { OrderId = 10248 });
                // affectedRows 1
                Console.WriteLine($"{nameof(DapperQueryAndExecute)} sqlOrderDetailDelete: {affectedRows}");
                // count 3+1-1
                Console.WriteLine($"{nameof(DapperQueryAndExecute)} sqlOrderDetail: {orderDetail3.Count()}");
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
