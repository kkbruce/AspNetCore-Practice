using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DapperAsyncSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ConsoleKeyInfo cki;
            // Prevent example from ending if CTL+C is pressed.
            Console.TreatControlCAsInput = true;
            Console.WriteLine("Press 0 show Dapper Async demo menu and input key number to run demo.");
            Console.WriteLine("Press the Escape (Esc) key to quit. \n");

            do
            {
                PrintMenu();
                cki = Console.ReadKey();
                Console.WriteLine();
                //Console.Write($"You pressed {cki.KeyChar.ToString()}");

                int value;
                if (int.TryParse(cki.KeyChar.ToString(), out value))
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
                            DapperParameterDynamicAsync(connString);
                            break;
                    }
                }
            } while (cki.Key != ConsoleKey.Escape);
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
            Console.WriteLine("\t7. DapperParameterDynamicAsync");
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
        /// Use DynamicParameters add single or many parameter(s)
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static async void DapperParameterDynamicAsync(string connString)
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

                await connection.ExecuteAsync(uspName, parameter, commandType: CommandType.StoredProcedure);

                int returnValue = parameter.Get<int>("@ReturnValue");
                Console.WriteLine($"\t{nameof(DapperParameterDynamicAsync)} ReturnValue: {returnValue} (0 is success.)");

                Console.WriteLine("Insert Many data:");
                var parameters = new List<DynamicParameters>();
                for (var i = 0; i < 3; i++)
                {
                    var p = new DynamicParameters();
                    p.Add("@ProductName", "DapperParameterDynamicTest"+(i+1), DbType.String, ParameterDirection.Input);
                    p.Add("@Discontinued", i, DbType.Int32, ParameterDirection.Input);
                    p.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    parameters.Add(p);
                }

                await connection.ExecuteAsync(uspName, parameters, commandType: CommandType.StoredProcedure);
                int returnValues = parameters.Sum(x => x.Get<int>("@ReturnValue"));
                Console.WriteLine($"\t{nameof(DapperParameterDynamicAsync)} ReturnValues: {returnValues} (0 is success.)");

            }
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
