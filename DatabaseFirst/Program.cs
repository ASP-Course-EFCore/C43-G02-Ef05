using DatabaseFirst.dbContexts;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DatabaseFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region Part 06 Database First [Command]
            //It's an approach where you start with a pre-existing database, and EF Core generates the models, DbContext, and relationships.
            //
            //Database First Approach Steps : 
            // 1.Install EF Core Packages [Microsoft.EntityFrameworkCore.SqlServer - Microsoft.EntityFrameworkCore.Tools]
            // 2.Scaffold the models and DbContext [With "Commands" Or "EfCore Power Tool"]

            //01. Using [Commands] To Scaffold Models Of Remote Database And the DbContext
            // Scaffold-DbContext -Connection "Server = .; Database = NorthWind; Trusted_Connection = true; TrustServerCertificate = true" -Provider "Microsoft.EntityFrameWorkCore.SqlServer" -Context "MyNorthWindDbContext" -ContextDir "dbContexts" -OutputDir "Models"

            ///Test Retrieve Data.
            ///MyNorthWindDbContext dbContext = new MyNorthWindDbContext();
            ///var products = dbContext.Products;
            ///
            ///foreach (var product in products)
            ///{
            ///    Console.WriteLine(product.ProductName);
            ///}
            
            //02. Using Tool[Extension] -> EfCore Power Tool
            //  This is Visual Studio extension that simplifies common EF Core tasks like reverse engineering databases,
            //  viewing models, and managing migrations. especially useful for Database First workflows ->
            //  Generating DbContext and models from an existing database.
            
            #endregion
        }
    }
}
