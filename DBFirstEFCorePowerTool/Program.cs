using DBFirstEFCorePowerTool.dbContexts;
using Microsoft.EntityFrameworkCore;

namespace DBFirstEFCorePowerTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Part 07 Database First [EF Core Power Tool]

            ///02. Scaffolding Using Tool[Extension] -> EfCore Power Tool
            ///  This is Visual Studio extension that simplifies common EF Core tasks like reverse engineering databases,
            ///  viewing models, and managing migrations. especially useful for Database First workflows ->
            ///  Generating DbContext and models from an existing database. 
            ///Note -> After make Scaffolding and try to add like new DB object from DB to Application
            ///The All files will be regenerated Again.
            ///if you make configuration in the generated files after make scaffolding, it will lost if you try to make reverse engineering on the DB again.
            ///If you make configurations in separated file, it will not lost.

            //using NorthwindDbContext dbContext = new NorthwindDbContext();

            //var employees = dbContext.Employees;

            //if(employees is not null)
            //{
            //    foreach (var employee in employees)
            //    {
            //        Console.WriteLine(employee.FirstName);
            //    }
            //}

            #endregion

            #region Part 08 Run Sql Raw
            ///Running/Execute raw SQL query - not use LinQ.
            ///
            ///For DQL [Data Query Language]
            /// 1.FromSqlRaw() - Write Sql Query as string without any Interpolated formatting
            /// 2.FromSqlInterpolated() - Write Sql Query as Formattable string $"" with Interpolated formatting
            ///
            ///For DML [Data Manipulation Language]
            /// 1.ExecuteSqlRaw()
            /// 2.ExecuteSqlInterpolated()
            ///
            ///Note -> Those "DML" Functions return "int" represent the number of rows affects In DB after execute it.

            #region Example01 - DQL statements like "Select" Using FromSqlRaw().

            //using NorthwindDbContext dbContext = new NorthwindDbContext();
            //int catId = 1;
            //var resultRaw = dbContext.Products.FromSqlRaw("Select * From Products where CategoryId = {0}", catId);
            //foreach (var product in resultRaw)
            //{
            //    Console.WriteLine(product.ProductName);
            //}
            //Console.WriteLine(); 

            #endregion

            #region Example02 - DQL statements like "Select" Using FromSqlInterpolated().

            //using NorthwindDbContext dbContext = new NorthwindDbContext();
            //var resultInterpolated = dbContext.Products.FromSqlInterpolated($"Select * From Products where CategoryId = {catId}");

            //foreach (var product in resultInterpolated)
            //{
            //    Console.WriteLine(product.ProductName);
            //} 

            #endregion

            #region Example03 - DML statements like "Update" Using ExecuteSqlRaw()
            //using NorthwindDbContext dbContext = new NorthwindDbContext();

            //int productId = 1;
            //var result = dbContext.Database.ExecuteSqlRaw("Update Products Set ProductName = 'Coffee' Where ProductId = {0}",productId);
            //Console.WriteLine($"Number of rows Affected -> {result}");//Number of rows Affected -> 1

            #endregion

            #region Example04 - DML statements like "Delete" Using ExecuteSqlInterpolated()
            //using NorthwindDbContext dbContext = new NorthwindDbContext();

            //string productName = "EgyptShai";
            //var result = dbContext.Database.ExecuteSqlInterpolated($"Delete From Products where ProductName = {productName}");
            //Console.WriteLine($"Number of rows Affected -> {result}");//Number of rows Affected -> 1

            #endregion

            //This make code tightly coupled to specific Database provider like SqlServer
            //Because you write syntax of SqlServer
            //So you can't use this APP/Project with another Database provider
            //So We don't use this way - We query DB objects using the LinQ operators.
            
            #endregion

        }
    }
}
