using System;
using System.Data;
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
                    }
                }
            } while (cki.Key != ConsoleKey.Escape);
        }

        private static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Dapper  Demo List(\"Esc\" key to quit):");
            Console.WriteLine("\t1. DapperQueryAsync");
            Console.WriteLine("\t2. DapperQueryAsyncStoredProcedure");

        }

        private static async Task DapperQueryAsync(string connString)
        {
            string sqlProducts = "SELECT * FROM Products;";
            using (var connection = new SqlConnection(connString))
            {
                var products = await connection.QueryAsync(sqlProducts).ConfigureAwait(false);
                Console.WriteLine($"Product counter: {products.AsList().Count}");
            };
        }

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
