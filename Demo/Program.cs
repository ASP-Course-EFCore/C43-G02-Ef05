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

            #region Part 02 Inheritance Mapping [TPCT] - Table Pair Concrete Type.
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
        }
    }
}
