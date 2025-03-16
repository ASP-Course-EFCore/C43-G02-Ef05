using DBFirstEFCorePowerTool.dbContexts;

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
        }
    }
}
