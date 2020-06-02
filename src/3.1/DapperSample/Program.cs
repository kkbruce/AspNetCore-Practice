using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DapperSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            // Prevent example from ending if CTL+C is pressed.
            Console.TreatControlCAsInput = true;
            Console.WriteLine("Press 0 show Dapper demo menu and input key number to run demo.");
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
                            DapperQueryAndExecute(connString);
                            break;
                        case 2:
                            DapperStoredProcedure(connString);
                            break;
                        case 3:
                            DapperQueryAnonymous(connString);
                            break;
                        case 4:
                            DapperQueryStronglyTyped(connString);
                            break;
                        case 5:
                            DapperQueryFirst(connString);
                            break;
                        case 6:
                            DapperQueryMultiple(connString);
                            break;
                        case 7:
                            DapperParameterDynamic(connString);
                            break;
                        case 8:
                            DapperParameterList(connString);
                            break;
                    }
                }
            } while (cki.Key != ConsoleKey.Escape);
        }

        private static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Dapper Demo List(\"Esc\" key to quit):");
            Console.WriteLine("\t1. DapperQueryAndExecute");
            Console.WriteLine("\t2. DapperStoredProcedure");
            Console.WriteLine("\t3. DapperQueryAnonymous");
            Console.WriteLine("\t4. DapperQueryStronglyTyped");
            Console.WriteLine("\t5. DapperQueryFirst");
            Console.WriteLine("\t6. DapperQueryMultiple");
            Console.WriteLine("\t7. DapperParameterDynamic");
            Console.WriteLine("\t8. DapperParameterList");
        }

        /// <summary>
        ///  Specify multiple parameter on t-sql IN
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static void DapperParameterList(string connString)
        {
            var sql = "SELECT * FROM [Customers] WHERE City IN @City;";
            using (var connection = new SqlConnection(connString))
            {
                var customerses = connection.Query<Customers>(sql,new { City = new[] {"Berlin", "London"}}).ToList();
                Console.WriteLine($"{nameof(DapperParameterList)} Customer counter: {customerses.Count}");
            }
        }

        /// <summary>
        /// Use DynamicParameters add single or many parameter(s)
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static void DapperParameterDynamic(string connString)
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

                connection.Execute(uspName, parameter, commandType: CommandType.StoredProcedure);

                int returnValue = parameter.Get<int>("@ReturnValue");
                Console.WriteLine($"\t{nameof(DapperParameterDynamic)} ReturnValue: {returnValue} (0 is success.)");

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

                connection.Execute(uspName, parameters, commandType: CommandType.StoredProcedure);
                int returnValues = parameters.Sum(x => x.Get<int>("@ReturnValue"));
                Console.WriteLine($"\t{nameof(DapperParameterDynamic)} ReturnValues: {returnValues} (0 is success.)");

            }
        }


        /// <summary>
        /// Execute multiple queries
        /// </summary>
        /// <param name="connString">Connection String</param>
        private static void DapperQueryMultiple(string connString)
        {
            string sqlMultiple = "SELECT * FROM Products WHERE ProductID = @ProductId; SELECT * FROM [Order Details] WHERE ProductID = @ProductId;";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();

                using (var queryMultiple = connection.QueryMultiple(sqlMultiple, new { ProductId = 1 }))
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
