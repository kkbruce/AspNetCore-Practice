using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

namespace DapperAsyncSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press 0 show Dapper demo menu and input number to run demo.");
            Console.WriteLine("Press the CTL+C key to quit. \n");

            do
            {
                PrintMenu();
                string input = Console.ReadLine();
                Console.WriteLine();

                if (int.TryParse(input, out var value))
                {
                    var config = Startup.Configuration(args);
                    var connString = config.GetConnectionString("NorthwindDatabase");

                    switch (value)
                    {
                        case 0:
                            PrintMenu();
                            break;
                        case 1:
                            await DapperQueryAsync(connString);
                            break;
                        case 2:
                            await DapperQueryAsyncStoredProcedure(connString);
                            break;
                        case 3:
                            await DapperQueryAsyncAnonymous(connString);
                            break;
                        case 4:
                            await DapperQueryAsyncStronglyTyped(connString);
                            break;
                        case 5:
                            await DapperQueryFirstAsync(connString);
                            break;
                        case 6:
                            DapperQueryMultipleAsync(connString);
                            break;
                        case 7:
                            await DapperExecuteAsync(connString);
                            break;
                        case 8:
                            await DapperParameterDynamicAsync(connString);
                            break;
                        case 9:
                            await DapperParameterListAsync(connString);
                            break;
                        case 10:
                            await DapperTransactionAsync(connString);
                            break;
                    }
                }
            } while (true);
        }

        private static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Dapper Async Demo List(\"Esc\" key to quit):");
            Console.WriteLine("\t1. DapperQueryAsync");
            Console.WriteLine("\t2. DapperQueryAsyncStoredProcedure");
            Console.WriteLine("\t3. DapperQueryAsyncAnonymous");
            Console.WriteLine("\t4. DapperQueryAsyncStronglyTyped");
            Console.WriteLine("\t5. DapperQueryFirstAsync");
            Console.WriteLine("\t6. DapperQueryMultipleAsync");
            Console.WriteLine("\t7. DapperExecuteAsync");
            Console.WriteLine("\t8. DapperParameterDynamicAsync");
            Console.WriteLine("\t9. DapperParameterListAsync");
            Console.WriteLine("\t10. DapperTransactionAsync");
        }

        /// <summary>
        /// Basic QueryAsync
        /// </summary>
        /// <param name="connString">Connection String</param>
        /// <returns></returns>
        private static async Task DapperQueryAsync(string connString)
        {
            string sqlProducts = "SELECT * FROM Products;";
            using (var connection = new SqlConnection(connString))
            {
                var products = await connection.QueryAsync(sqlProducts).ConfigureAwait(false);
                Console.WriteLine($"Product counter: {products.AsList().Count}");
            };
        }

        /// <summary>
        /// QueryAsync by Stored Procedure
        /// </summary>
        /// <param name="connString">Connection String</param>
        /// <returns></returns>
        private static async Task DapperQueryAsyncStoredProcedure(string connString)
        {
            string uspName = "CustOrdersDetail";

            using (var connection = new SqlConnection(connString))
            {
                var result = await connection.QueryAsync<CustOrdersDetail>(
                    uspName, new { OrderId = 10248 }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                Console.WriteLine($"{nameof(DapperQueryAsyncStoredProcedure)}: {result.AsList().Count}");
                result.AsList().ForEach(c =>
                    Console.WriteLine($"\t{nameof(DapperQueryAsyncStoredProcedure)}: {c.ProductName},{c.UnitPrice},{c.Quantity},{c.Discount},{c.ExtendedPrice}"));
            }
        }

        /// <summary>
        /// QueryAsync Anonymous(dynamic type)
        /// </summary>
        /// <param name="connString">Connection String</param>
        /// <returns></returns>
        private static async Task DapperQueryAsyncAnonymous(string connString)
        {
            string sqlProducts = "SELECT * FROM Products;";
            using (var connection = new SqlConnection(connString))
            {
                var products = await connection.QueryAsync(sqlProducts).ConfigureAwait(false);
                var product = products.AsList().FirstOrDefault();
                Console.WriteLine($"{nameof(DapperQueryAsyncAnonymous)}: {product}");
            }
        }

        /// <summary>
        /// QueryAsync by Strongly Typed
        /// </summary>
        /// <param name="connString">Connection String</param>
        /// <returns></returns>
        private static async Task DapperQueryAsyncStronglyTyped(string connString)
        {
            string sqlProducts = "SELECT * FROM Products;";
            using (var connection = new SqlConnection(connString))
            {
                var products = await connection.QueryAsync<Products>(sqlProducts).ConfigureAwait(false);
                Console.WriteLine($"{nameof(DapperQueryAsyncStronglyTyped)}: {products.AsList().Count}");
                var product = products.AsList().FirstOrDefault();
                Console.WriteLine($"{nameof(DapperQueryAsyncStronglyTyped)}: First ProductName is {product.ProductName}");
            }
        }

        /// <summary>
        /// QueryFirstAsync, QuerySingleAsync, QueryFirstOrDefaultAsync, QuerySingleOrDefaultAsync
        /// </summary>
        /// <param name="connString">Connection String</param>
        /// <returns></returns>
        private static async Task DapperQueryFirstAsync(string connString)
        {
            string sqlProducts = "SELECT * FROM Products WHERE ProductID = @ProductId;";
            using (var connection = new SqlConnection(connString))
            {
                // Query Anonymous
                var productAnonymous = await connection.QueryFirstAsync(sqlProducts, new { ProductId = 1 }).ConfigureAwait(false);
                Console.WriteLine($"{nameof(DapperQueryFirstAsync)} Anonymous: {productAnonymous}");

                // Strongly Typed
                var productFirst = await connection.QueryFirstAsync<Products>(sqlProducts, new { ProductId = 1 }).ConfigureAwait(false);
                Console.WriteLine($"{nameof(DapperQueryFirstAsync)} QueryFirstAsync Id: {productFirst.ProductId}, Name: {productFirst.ProductName}");

                var productSingle = await connection.QuerySingleAsync<Products>(sqlProducts, new { ProductId = 1 }).ConfigureAwait(false);
                Console.WriteLine($"{nameof(DapperQueryFirstAsync)} QuerySingleAsync Id: {productSingle.ProductId}, Name: {productSingle.ProductName}");

                var productFirstOrDefault = await connection.QueryFirstOrDefaultAsync<Products>(sqlProducts, new { ProductId = 1 }).ConfigureAwait(false);
                Console.WriteLine($"{nameof(DapperQueryFirstAsync)} QueryFirstOrDefaultAsync Id: {productFirstOrDefault.ProductId}, Name: {productFirstOrDefault.ProductName}");

                var productSingleOrDefault = await connection.QuerySingleOrDefaultAsync<Products>(sqlProducts, new { ProductId = 1 }).ConfigureAwait(false);
                Console.WriteLine($"{nameof(DapperQueryFirstAsync)} QuerySingleOrDefaultAsync Id: {productSingleOrDefault.ProductId}, Name: {productSingleOrDefault.ProductName}");
            }
        }

        /// <summary>
        /// Execute QueryMultipleAsync
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static void DapperQueryMultipleAsync(string connString)
        {
            string sqlMultiple = "SELECT * FROM Products WHERE ProductID = @ProductId; SELECT * FROM [Order Details] WHERE ProductID = @ProductId;";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();

                using (var queryMultiple = connection.QueryMultipleAsync(sqlMultiple, new { ProductId = 1 }).Result)
                {
                    // Execute first SELECT
                    var products = queryMultiple.Read<Products>().First();
                    // Execute second SELECT
                    var orderDetails = queryMultiple.Read<OrderDetail>().ToList();

                    Console.WriteLine($"{nameof(DapperQueryMultipleAsync)} first: {products.ProductName}");
                    Console.WriteLine($"{nameof(DapperQueryMultipleAsync)} second: {orderDetails.Count}");
                }
            }
        }

        /// <summary>
        /// Execute for "UPDATE", "INSERT", "DELETE" T-SQL.
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static async Task DapperExecuteAsync(string connString)
        {
            string sqlProducts = "SELECT * FROM Products;";
            string sqlOrderDetail = "SELECT * FROM [Order Details] WHERE OrderId = @OrderId;";
            string sqlOrderDetailInsert =
                "INSERT INTO [Order Details] ([OrderID],[ProductID],[UnitPrice],[Quantity],[Discount]) VALUES (@OrderID,@ProductID,@UnitPrice,@Quantity,@Discount);";
            string sqlOrderDetailDelete =
                "DELETE FROM [Order Details] WHERE [OrderID] = @OrderId AND [ProductID] = @ProductId;";

            using (var connection = new SqlConnection(connString))
            {
                var products = await connection.QueryAsync<Products>(sqlProducts).ConfigureAwait(false);
                var orderDetail = await connection.QueryAsync<OrderDetail>(sqlOrderDetail, new { OrderId = 10248 }).ConfigureAwait(false);
                var affectedRows = await connection.ExecuteAsync(sqlOrderDetailInsert, new
                {
                    OrderId = 10248,
                    ProductID = 1,
                    UnitPrice = 0.0,
                    Quantity = 1,
                    Discount = 0
                }).ConfigureAwait(false);
                var orderDetail2 = await connection.QueryAsync<OrderDetail>(sqlOrderDetail, new { OrderId = 10248 }).ConfigureAwait(false);

                // count 77
                Console.WriteLine($"{nameof(DapperExecuteAsync)} sqlProducts: {products.Count()}");
                // count 3
                Console.WriteLine($"{nameof(DapperExecuteAsync)} sqlOrderDetail: {orderDetail.Count()}");
                // affectedRows 1
                Console.WriteLine($"{nameof(DapperExecuteAsync)} sqlOrderDetailInsert: {affectedRows}");
                // count 3+1
                Console.WriteLine($"{nameof(DapperExecuteAsync)} sqlOrderDetail: {orderDetail2.Count()}");

                affectedRows = await connection.ExecuteAsync(sqlOrderDetailDelete, new { OrderId = 10248, ProductId = 1 }).ConfigureAwait(false);
                var orderDetail3 = await connection.QueryAsync<OrderDetail>(sqlOrderDetail, new { OrderId = 10248 }).ConfigureAwait(false);
                // affectedRows 1
                Console.WriteLine($"{nameof(DapperExecuteAsync)} sqlOrderDetailDelete: {affectedRows}");
                // count 3+1-1
                Console.WriteLine($"{nameof(DapperExecuteAsync)} sqlOrderDetail: {orderDetail3.Count()}");
            }
        }

        /// <summary>
        /// Use DynamicParameters add single or many parameter(s)
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static async Task DapperParameterDynamicAsync(string connString)
        {
            // First, you need run Usp_Insert_Products.sql in Northwind
            // Because Northwind no any CUD Stored Procedure
            string uspName = "Usp_Insert_Products";

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();


                Console.WriteLine("Insert single data:");
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@ProductName", "DapperParameterDynamicTest", DbType.String, ParameterDirection.Input);
                parameter.Add("@Discontinued", 0, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await connection.ExecuteAsync(uspName, parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                int returnValue = parameter.Get<int>("@ReturnValue");
                Console.WriteLine($"\t{nameof(DapperParameterDynamicAsync)} ReturnValue: {returnValue} (0 is success.)");

                Console.WriteLine("Insert Many data:");
                var parameters = new List<DynamicParameters>();
                for (var i = 0; i < 3; i++)
                {
                    var p = new DynamicParameters();
                    p.Add("@ProductName", "DapperParameterDynamicTest" + (i + 1), DbType.String, ParameterDirection.Input);
                    p.Add("@Discontinued", i, DbType.Int32, ParameterDirection.Input);
                    p.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    parameters.Add(p);
                }

                await connection.ExecuteAsync(uspName, parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                int returnValues = parameters.Sum(x => x.Get<int>("@ReturnValue"));
                Console.WriteLine($"\t{nameof(DapperParameterDynamicAsync)} ReturnValues: {returnValues} (0 is success.)");

            }
        }

        /// <summary>
        /// Specify multiple parameter on t-sql IN
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static async Task DapperParameterListAsync(string connString)
        {
            var sql = "SELECT * FROM [Customers] WHERE City IN @City;";
            using (var connection = new SqlConnection(connString))
            {
                var customerses = await connection.QueryAsync<Customers>(sql, new { City = new[] { "Berlin", "London" } }).ConfigureAwait(false);
                Console.WriteLine($"{nameof(DapperParameterListAsync)} Customer counter: {customerses.AsList().Count}");
            }
        }

        /// <summary>
        /// ExecuteAsync by TransactionAsync
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static async Task DapperTransactionAsync(string connString)
        {
            string updateProducts = "UPDATE [Products] SET [ProductName] = @ProductName WHERE [ProductID] = @ProductId;";
            string sqlProduct = "SELECT * FROM Products WHERE ProductID = @ProductId;";
            using (var connection = new SqlConnection(connString))
            {
                await connection.OpenAsync();
                await using (var transaction = await connection.BeginTransactionAsync())
                {
                    int affectedRows = await connection.ExecuteAsync(updateProducts, new { ProductId = 1, ProductName = "Transaction Update" }, transaction: transaction).ConfigureAwait(false);
                    await transaction.CommitAsync();

                    Console.WriteLine($"{nameof(DapperTransactionAsync)} Update affected rows: {affectedRows}");
                }

                var product = await connection.QueryFirstAsync<Products>(sqlProduct, new { ProductId = 1 }).ConfigureAwait(false);
                Console.WriteLine($"{nameof(DapperTransactionAsync)} Update ProductName: {product.ProductName}");

                var parameters = new[]
                {
                    new {ProductId = 1, ProductName = "Transaction Update Id1"},
                    new {ProductId = 2, ProductName = "Transaction Update Id2"},
                    new {ProductId = 3, ProductName = "Transaction Update id3"}
                };

                await using (var transaction = await connection.BeginTransactionAsync())
                {
                    int affectedRows = await connection.ExecuteAsync(updateProducts, parameters, transaction: transaction).ConfigureAwait(false);
                    await transaction.CommitAsync();

                    Console.WriteLine($"{nameof(DapperTransactionAsync)} Update affected rows: {affectedRows}");
                }
            }
        }

        internal class Customers
        {
            public string CustomerId { get; set; }
            public string CompanyName { get; set; }
            public string ContactName { get; set; }
            public string ContactTitle { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Region { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
        }

        internal class OrderDetail
        {
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public decimal UnitPrice { get; set; }
            public short Quantity { get; set; }
            public float Discount { get; set; }
        }

        internal class CustOrdersDetail
        {
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public short Quantity { get; set; }
            public float Discount { get; set; }
            public decimal ExtendedPrice { get; set; }

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
