using Demo.DbContexts;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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

            #region Part 04 Inheritance Mapping [TPT] - Table-Per-Type
            //Each class in the inheritance hierarchy maps to a separate table 
            //Mean That Map "Employee" Class As Table And "FullTimeEmployee" As Table And "PartTimeEmployee" As Table
            //EF Core joins those tables when querying derived types.

            //So Inside The "MyCompanyDbContext" class, make property of type DbSet<T>
            //For each type 
            //   public DbSet<Employee> Employees { get; set; }
            //   public DbSet<FullTimeEmployee> FullTimeEmployees { get; set; }
            //   public DbSet<PartTimeEmployee> PartTimeEmployees { get; set; }
            //But if you make this and try to add migration 
            //The default behavior of EF Core to map this as [TPH] Strategy not [TPT].
            //Mean Map them in one table and add Discriminator column.
            //So we need to override this default behavior of mapping to one table when see property of type DbSet<parentClass> and another properties of type DbSet<ChildClass>
            //EF Core when see this -> Map them in one table
            //So We need to override this default behavior by write configuration "FluentAPIs" inside the OnModelCreating()
            //To say that the child classes also will mapped as separated table.
            //
            //    modelBuilder.Entity<FullTimeEmployee>()
            //              .ToTable("FullTimeEmployees");
            //    modelBuilder.Entity<PartTimeEmployee>()
            //                .ToTable("PartTimeEmployees");

            //After Make This And Add-Migration
            //EF Core Will Take "PK" column of table [Employees] "Parent Class/table"
            //As "FK" column in the child classes [FullTimeEmployees] - [PartTimeEmployees].
            //So now we have "FullTimeEmployees" table with columns "Id" - "Salary" - "StartDate"
            //This "Id" refer to existing "Id" in "employees" table and also "PK" off table "FullTimeEmployees"
            //And we have "PartTimeEmployees" table with columns "Id" - "HourRate" - "CountOfHours"
            //This "Id" refer to existing "Id" in "employees" table and also "PK" of table "PartTimeEmployees"

            //So with this approach we avoiding the "Null" by make table for the child types 
            //And attach them with the parent type by take PK "Id" of parent type as FK in the child types.
            //So every "FullTimeEmployee" record/row/object in the "FullTimeEmployees" table represent existing Employee in the "Employees" Table
            //So every "PartTimeEmployee" record/row/object in the "PartTimeEmployees" table represent existing Employee in the "Employees" Table

            //So when add new "FullTimeEmployee" object in DB with these columns values -> 
            //Name = "Eslam" - Age = 22 - Address = "Mansoura" - Salary = 4000 - StartDate = DateTime.Now
            //We will found in DB that in the "Employees" Table  this is new Record Added in the columns with values
            //Id = 1 - Name = "Eslam" - Age = 22 - Address = "Mansoura"
            //And Inside the "FullTimeEmployees" Table we will found that new Record Added In the columns With values
            //Id = 1 - Salary = 4000 - StartDate = 2025-03-16 15:33:12.5700416

            //using MyCompanyDbContext dbContext = new MyCompanyDbContext();
            //FullTimeEmployee fullTimeEmployee = new FullTimeEmployee()
            //{
            //    Name = "Eslam",
            //    Age = 22,
            //    Address = "Mansoura",
            //    Salary = 4000,
            //    StartDate = DateTime.Now
            //};
            //PartTimeEmployee partTimeEmployee = new PartTimeEmployee()
            //{
            //    Name = "Heba",
            //    Age = 20,
            //    Address = "Mansoura",
            //    HourRate = 50,
            //    CountOfHours = 10
            //};

            ///01 - Add FullTime Employee object/Record
            ///
            //dbContext.Add<FullTimeEmployee>(fullTimeEmployee);
            //it's like add in Employees Table object of type FullTimeEmployee ->
            //dbContext.Add<Employee>(fullTimeEmployee);

            ///02 - Add PartTime Employee object/Record
            ///
            //dbContext.Add<PartTimeEmployee>(partTimeEmployee);
            //it's like add in Employees Table object of type PartTimeEmployee ->
            //dbContext.Add<Employee>(partTimeEmployee);

            //dbContext.SaveChanges();

            //So Now You Have 3 options ->
            //Deal with Table Employees Only
            //Deal with Table FullTimeEmployees Only
            //Deal with Table PartTimeEmployees Only

            #region Example01 - Retrieve Data Of Employees Table [FullTime & PartTime]

            //var Employees = (from E in dbContext.Employees
            //                select E).ToList();//Immediate Execution.[Retrieve All Data First]

            //if(Employees is not null)
            //{
            //    foreach (var item in Employees)
            //    {
            //        Console.WriteLine($"{item.Name}::{item.Age}::{item.Address}");//Eslam::22::Mansoura
            //                                                                            //Heba::20::Mansoura
            //    }
            //}

            #endregion

            #region Example02 - Retrieve Data Of FullTimeEmployee Table .

            //var Employees =from E in dbContext.Employees
            //                 select E;//Deferred Execution -> This query will not executed until use "Employees" variable
            //                          // So This is one query will executed to get data of FullTimeEmployee not 2 queries like previous to retreive data of Employees Table then filter.

            //if (Employees is not null)
            //{
            //    foreach (var item in Employees.OfType<FullTimeEmployee>())
            //    {
            //        Console.WriteLine($"{item.Name}::{item.Age}::{item.Address}::{item.Salary}::{item.StartDate}");
            //        //Eslam::22::Mansoura::4000.00::3/16/2025 5:15:17 PM                                                              
            //    }
            //}

            #endregion

            #region Example03 - Retrieve Data Of FullTimeEmployee Table .

            //var Employees = from E in dbContext.Employees
            //                select E;//Deferred Execution -> This query will not executed until use "Employees" variable
            //                         // So This is one query will executed to get data of PartTimeEmployee not 2 queries like previous to retrieve data of Employees Table then filter.

            //if (Employees is not null)
            //{
            //    foreach (var item in Employees.OfType<PartTimeEmployee>())
            //    {
            //        Console.WriteLine($"{item.Name}::{item.Age}::{item.Address}::{item.HourRate}::{item.CountOfHours}");
            //        //Heba::20::Mansoura::50.00::10                                                             
            //    }
            //}

            #endregion

            #endregion

            ///Use [TPCT] -> If you need to deal with FullTimeEmployees Table & PartTimeEmployees Table as seperate from each other
            ///And don't care about repeated columns "Id" - "Age" - "Address" in the 2 tables.
            ///
            ///Use [TPH] -> If You Need to deal with Employees in one table Regardless of Types "FullTimEmployee" - "PartTimEmployee"
            ///And Distinguished between them based on the Discriminator column
            ///And if you don't care about "Nulls" in the data.
            ///
            ///Use [TPT] -> If You need to deal with each type as separate table
            ///And Common Columns are in the base class.

            #region Part 05 Local & Load
            //using MyCompanyDbContext dbContext = new MyCompanyDbContext();

            #region Ex01 - Return Employees with Age != Null using Local and without use it.

            //var result01 = dbContext.Employees.Any(E => E.Age != null);
            ////This Line Will Send Request To DB To Check If there is any Employee object
            ////In Employees Table that it's age column not "Null" and return true or false.
            ////In This Line You Communicate With "Employees" table in Database not in Local[App].
            //Console.WriteLine($"Remote = {result01}");//True
            ////"True" Because There are 2 Employees objects in Employees Table That have Age != Null.

            //var result02 = dbContext.Employees.Local.Any(E => E.Age != null);
            ////No Database Interaction [There is no request sent to DB].
            ////It will search locally in the data that loaded in the DbSet<Employee> Employees.
            //Console.WriteLine($"Remote = {result02}");//False
            ////"False" Because I not loaded any Employees objects inside this DbSet<Employee>.

            #endregion

            #region Ex02 - Return First Employee and check if there is employee with age == 22 Local & Remote.

            //var employee01 = dbContext.Employees.FirstOrDefault();//This Employee object stored locally inside "DbSet<Employee> Employees"
            //if(employee01 is not null)
            //{
            //    Console.WriteLine(employee01.Age);//22
            //    employee01.Age = 25;//Edit Locally - Not Affect the DB because i don't make SaveChanges().
            //}

            //var LocalResult = dbContext.Employees.Local.Any(E => E.Age == 22);//Search Locally in "DbSet<Employee>"
            //Console.WriteLine(LocalResult);//False. [Because There is no Employee object Returned Locally with Age = 22 - I edit Age of returned object locally to "25"]

            //var remoteResult = dbContext.Employees.Any(E => E.Age == 22);//Search Remote in Database in table "Employees".
            //Console.WriteLine(remoteResult);//True. [Because There is Employee object In Database with Age = 22]

            #endregion

            #region Ex03 - Use Load() To Load data of specific table locally.

            //dbContext.Employees.Load();//All Employees objects/records in table Employees loaded locally by send request to DB.

            //var oldEmployee = dbContext.Employees.FirstOrDefault(E => E.Age > 25);

            //if(oldEmployee is not null)
            //    Console.WriteLine(oldEmployee.Name);//Heba

            #endregion

            //So I use Local Keyword to search on the loaded data locally instead of send requests to Database.

            #endregion

        }
    }
}
