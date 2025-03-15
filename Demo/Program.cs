using Demo.DbContexts;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ///Inheritance Mapping =>
            ///Like If we have Class "Full Time Employee" that has properties -> "Id" - "Name" - "Age" - "Address" - "StartDate" - "Salary"
            ///And another class     "Part Time Employee" that has properties -> "Id" - "Name" - "Age" - "Address" - "HourRate" - "CountOfHour"
            ///So There are common properties, So We will make base class "Employee" that will has the common properties -> "Id" - "Name" - "Age" - "Address
            ///And make "FullTimeEmployee" class which has only the different properties like "StartDate" - "Salary" and make it inherit from class "Employee".
            ///And make "PartTimeEmployee" class which has only the different properties like "HourRate" - "CountOfHour" and make it inherit from class "Employee".
            ///
            ///So How to map this inheritance relationship To Database?
            ///There are 3 Ways/Approaches For Mapping this inheritance relationship.
            ///  1.Table-Per-Concrete-Type (TPC) [Make table for the concrete class [كلاس يكون كامل وبيمثل انتيتي زي الفول تايم امبلويي والبارت تايم] 
            ///  2.Table-Per-Hierarchy (TPH)     [Make one table for the 3 Tables of inheritance relationship].
            ///  3.Table-Per-Type (TPT)          [Make Table For every class/Type not for concrete type only].

            #region Part 02 Inheritance Mapping [TPCT] - Table Per Concrete Type.
            ///Each Concrete Class Has it's own table, No table for base classes - only tables for concrete types.
            ///First Make The Models -> [Employee - FullTimeEmployee - PartTimeEmployee]
            ///And Make [FullTimeEmployee - PartTimeEmployee] inherit the common properties from the base class "Employee"
            ///Then You need connection With Database, so make new DbContext Class and make it inherit from DbContextClass
            ///And Add the connection string for connecting with DB inside the OnConfiguring() method.
            ///And install sqlServer package and EFCORE Tools package to deal with DB and migrations.
            ///And Then Choose you need to work by which strategy to map this inheritance relationship [TPCT - TPH - TPT].
            ///Now we will work with approach "TPCT" -> Table per concrete type. 
            ///So we will define only 2 properties of type "DbSet<FullTimeEmployee> FullTimeEmployees" - "DbSet<PartTimeEmployee> PartTimeEmployees" inside the DbContext class
            ///To map them into the Database and not map the Base class.
            ///And Then Add-Migration
            ///
            ///We will found that this migration make new 2 table "FullTimeEmployees" - "PartTimeEmployees" [Configuring By Convention]
            ///"FullTimeEmployees" has columns -> "Id" - "Name" - "Age" - "Address" - "Salary" - "StartDate"
            ///"PartTimeEmployees" has columns -> "Id" - "Name" - "Age" - "Address" - "HourRate" - "CountOfHours"
            ///Then Update-Database to add those columns as tables in the DB.
            ///
            ///So Now I need to insert data into those tables in DB -> [make manual seeding by make object from those types and add this object in DB and then SaveChanges() after open connection with DB].

            //FullTimeEmployee fullTimeEmployee= new FullTimeEmployee()
            //{
            //    Name = "Soha",
            //    Age = 25,
            //    Address = "Cairo",
            //    Salary = 20000,
            //    StartDate = DateTime.Now
            //};

            //PartTimeEmployee partTimeEmployee = new PartTimeEmployee()
            //{
            //    Name = "Amr",
            //    Age = 30,
            //    Address = "Giza",
            //    HourRate = 100,
            //    CountOfHours = 30
            //};

            //using MyCompanyDbContext dbContext = new MyCompanyDbContext();
            ////dbContext.Add<FullTimeEmployee>(fullTimeEmployee);
            ////dbContext.Add<PartTimeEmployee>(partTimeEmployee);
            //dbContext.SaveChanges();

            ///In this strategy (TPCT) you deal with every concrete class/Table as specific table [ملهمش علاقه ببعض ومبيتعاملوش مع بعض كان مفيش علاقة توريث]
            ///Every concrete type is separate from the another concrete type.

            ////Try To Retrieve data from the 2 tables -> 
            //var FTEmployee = dbContext.FullTimeEmployees.AsNoTracking().FirstOrDefault();
            //var PTEmployee = (from PTE in dbContext.PartTimeEmployees.AsNoTracking()
            //                 select PTE).FirstOrDefault();

            //if(FTEmployee is not null)
            //    Console.WriteLine($"{FTEmployee.Id} :: {FTEmployee.Name} :: {FTEmployee.Age} :: {FTEmployee.Address} :: {FTEmployee.Salary:c} :: {FTEmployee.StartDate}");//1 :: Soha :: 25 :: Cairo :: $20,000.00 :: 3/14/2025 11:39:08 PM

            //if (PTEmployee is not null)
            //    Console.WriteLine($"{PTEmployee.Id} :: {PTEmployee.Name} :: {PTEmployee.Age} :: {PTEmployee.Address} :: {PTEmployee.HourRate} :: {PTEmployee.CountOfHours}");//1 :: Amr :: 30 :: Giza :: 100.00 :: 30

            ///So To retrieve data from each table -> You need to make separate query for each table 
            ///Those 2 tables not have any relationship in database.

            #endregion

            #region Part 03 Inheritance Mapping [TPH] - Table-Per-Hierarchy
            //Map This inheritance Hierarchy -> BaseClass "Employee" + ChildClass "FullTimeEmployee" + ChildClass "PartTimeEmployee" As one table.
            //(TPH) is the default strategy in EF Core, All Classes in an inheritance hierarchy are mapped to a single table and a "discriminator" column is used to distinguished between different types "FullTimeEmployee" - "PartTimeEmployee".
            //
            //Steps => 
            //1. Make Property of type DbSet of base class to represent the table Employees -> public DbSet<Employee> Employees { get; set; }
            //2. Make Property of type DbSet of Child class to represent the table FullTimeEmployees -> public DbSet<FullTimeEmployee> FullTimeEmployees { get; set; }
            //3. Make Property of type DbSet of Child class to represent the table PartTimeEmployees -> public DbSet<PartTimeEmployee> PartTimeEmployees { get; set; }
            //
            //And By convention [Default] -> The EF Core Will map this inheritance hierarchy between those DbSets/Tables as only one table called "Employees" [Table Per Hierarchy]
            //After Add-Migration -> Found that EF Core make one Table "Employees" has columns of "Employee" Class && "FullTimeEmployee" class && "PartTimeEmployee" class + Discriminator column 
            //"Id" - "Name" - "Age" - "Address" - "Discriminator" - "Salary" - "StartDate" - "HourRate" - "CountOfHours"
            //This Discriminator column -> which of type string and hold values "FullTimeEmployee" - "PartTimeEmployee" is To distinguished between different types "FullTimeEmployee" - "PartTimeEmployee".
            //If you add inside the table "Employees" object of type "FullTimeEmployee" -> This Discriminator column will hold value "FullTimeEmployee"
            //If you add inside the table "Employees" object of type "PartTimeEmployee" -> This Discriminator column will hold value "PartTimeEmployee"
            //
            //This is by convention.
            //
            //What if you need to define one property "DbSet<Employee> employees" inside the DbContext class and not add the another tables for "FullTimeEmployee" + "PartTimeEmployee"
            //And at the same time you need to tell the EF Core that those table "Employee" + "FullTimeEmployee" + "PartTimeEmployee" are in the same Inheritance hierarchy
            //Mean that you need to tell it to map then in one table that has columns of all 3 tables + Discriminator column
            //  You can do this using Fluent APIs way ->
            //Make configuration for class "FullTimeEmployee" - "PartTimeEmployee" to say that those entities are has base class "Employee" inside function OnModelCreating() inside class "MyCompanyDbContext"
            //  protected override void OnModelCreating(ModelBuilder modelBuilder)
            //  {
            //      modelBuilder.Entity<FullTimeEmployee>()
            //                  .HasBaseType<Employee>();
            //  
            //      modelBuilder.Entity<PartTimeEmployee>()
            //                  .HasBaseType<Employee>();
            //  }
            //
            //So now you tell EF Core That "FullTimeEmployee" & "PartTimeEmployee" are child classes for base class "Employee" to map them inside one table.
            //
            //What if you need to hold the "Discriminator" column and make configuration on it 
            //Like if you need to change it's name or change it's default values "FullTimeEmployee" - "PartTimeEmployee".
            //Make This using Fluent APIs inside OnModelCreating() -> 
            //  protected override void OnModelCreating(ModelBuilder modelBuilder)
            //  {
            //      modelBuilder.Entity<Employee>()
            //                  .HasDiscriminator<string>("EmployeeType")
            //                  .HasValue<FullTimeEmployee>("FTE")
            //                  .HasValue<PartTimeEmployee>("PTE");
            //  }
            //
            //So now I change The "Discriminator" column name to "EmployeeType"
            //And Change the default string values that it can hold from "FullTimeEmployee" - "PartTimeEmployee" to "FTE" - "PTE"
            //
            //Then Add-Migration "TPHMigration"
            //And Update-Database
            //
            //Found that this Hierarchy relationship mapped as one table "Employees" which has all columns of the 3 tables + "EmployeeType" Column which is the Discriminator column hold values "FTE" || "PTE" to distigushed between FullTimeEmployee object/Row/Record and PartTimeEmployee object/Row/Record in Database.
            //
            //Note =>
            //1.When Add object of type "FullTimeEmployee" in Database
            //The value of columns of "PartTimeEmployee" class "HourRate" - "CountOfHours" will be set as "NULL"
            //2.When Add object of type "PartTimeEmployee" in Database
            //The value of columns of "PartTimeEmployee" class "Salary" - "StartDate" will be set as "NULL"

            //using MyCompanyDbContext dbContext = new MyCompanyDbContext();

            //FullTimeEmployee fullTimeEmployee = new FullTimeEmployee()
            //{
            //    Name = "Soha",
            //    Age = 25,
            //    Address = "Cairo",
            //    Salary = 20000,
            //    StartDate = DateTime.Now
            //};

            //PartTimeEmployee partTimeEmployee = new PartTimeEmployee()
            //{
            //    Name = "Amr",
            //    Age = 30,
            //    Address = "Giza",
            //    HourRate = 100,
            //    CountOfHours = 30
            //};

            //dbContext.Add<Employee>(fullTimeEmployee);//Add in the Employees Table
            ////dbContext.Employees.Add(fullTimeEmployee);//Add in the Employees Table
            ////dbContext.Set<FullTimeEmployee>().Add(fullTimeEmployee);//Add in the Employees Table
            //dbContext.Add<Employee>(partTimeEmployee);

            //dbContext.SaveChanges();

            #region Example01 - Return All Employees [FTE + PTE]

            //var Employees = dbContext.Employees;

            //foreach (var employee in Employees)
            //{
            //    Console.WriteLine($"{employee.Name} :: {employee.Age}");//Soha:: 25
            //                                                            //Amr:: 30 

            //} 

            #endregion

            ///You can't say {employee.Salary} || {employee.StartDate} || {employee.HourRate} || {employee.CountOfHours}
            ///Because the variable "employee" is of type class "Employee" mean it's object of type "Employee"
            ///Mean it can only access properties/columns of class/table employee -> "Id" - "Name" - "Age" - "Address".


            #region Example02 - Return Only FullTimeEmployees Records || PartTimeEmployees Records by first return all employees and then make filteration when make foreach to print [Make only one query].

            //var employees = dbContext.Employees.AsNoTracking().ToList();

            //foreach (var FTE in employees.OfType < FullTimeEmployee>())
            //{
            //    Console.WriteLine($"{FTE.Name} :: {FTE.Salary:c} :: {FTE.StartDate}");
            //}

            //foreach (var PTE in employees.OfType < PartTimeEmployee>())
            //{
            //    Console.WriteLine($"{PTE.Name} :: {PTE.HourRate} :: {PTE.CountOfHours}");
            //}

            ////Note => If you don't use ToList() "ImmediateExecution"
            ////This query of retrieving all employees will not executed in the line of define it (193)
            ////it will divided into 2 queries/SqlQueries to DB
            ////one for get the FullTimeEmployees Objects
            ////Another for get the PartTimeEmployees Objects
            ////
            ////So Use any immediate execution operator like ToList() 
            ////To Execute this query in the line of defining instead of make 2 queries to DB
            ////When using this Immediate execution operator -> you return all employees objects "FTE" + "PTE"
            ////And then in for each you make filteration to view only "FTE" or "PTE" without make new query to DB.

            #endregion

            #region Example03 - Return Only FullTimeEmployees Records || PartTimeEmployees Records by make sql query for each type 

            //var FTES = dbContext.Employees.AsNoTracking().OfType<FullTimeEmployee>();
            //foreach (var FTE in FTES)
            //{
            //    Console.WriteLine($"{FTE.Name} :: {FTE.Salary:c} :: {FTE.StartDate}");//Soha :: $20,000.00 :: 3/15/2025 1:12:47 AM
            //}


            //var PTES = dbContext.Employees.AsNoTracking().OfType<PartTimeEmployee>();
            //foreach (var PTE in PTES)
            //{
            //    Console.WriteLine($"{PTE.Name} :: {PTE.HourRate} :: {PTE.CountOfHours}");//Amr :: 100.00 :: 30
            //}

            #endregion

            ///So this strategy (TPH) - Deal with all inheritance hierarchy as one table 
            ///So when retrieve data from this table i will retrieve from one table but there are "Nulls".
            ///Use This Strategy (TPH) - If You Don't care about "Nulls" in the Database.
            ///Use This Strategy (TPH) - If You Need to deal with all employees [All Hierarchy] In one table.
            ///This Strategy is the default behavior of mapping Inheritance relationship in EF Core
            ///But in not use usually because it causes "Nulls".
            
            #endregion
        }
    }
}
