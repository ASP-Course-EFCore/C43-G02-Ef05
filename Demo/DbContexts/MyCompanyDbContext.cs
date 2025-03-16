using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DbContexts
{
    class MyCompanyDbContext:DbContext
    {
        #region [TPCT] Strategy - Make Table for every concrete class.
        //public DbSet<FullTimeEmployee> FullTimeEmployees { get; set; }
        //public DbSet<PartTimeEmployee> PartTimeEmployees { get; set; } 
        #endregion

        #region [TPH] Strategy - Map the 3 tables as one table for all inheritance hierarchy.
        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<FullTimeEmployee> FullTimeEmployees { get; set; }
        //public DbSet<PartTimeEmployee> PartTimeEmployees { get; set; } //Or Define Them As Children For base class "Employee" inside OnModelCreating().
        #endregion

        #region [TPT] Strategy - Make table for every type in the inheritance hierarchy. 
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FullTimeEmployee> FullTimeEmployees { get; set; }
        public DbSet<PartTimeEmployee> PartTimeEmployees { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = . ; Database = MyCompany ; Trusted_Connection = true ; TrustServerCertificate = true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Map (TPH - Table Per Hierarchy) Approach. 
            //
            ///01 ->
            ///modelBuilder.Entity<FullTimeEmployee>()
            ///            .HasBaseType<Employee>();
            ///
            ///modelBuilder.Entity<PartTimeEmployee>()
            ///            .HasBaseType<Employee>();

            ///02-> [To Change The Discriminator column name].
            ///modelBuilder.Entity<Employee>()
            ///            .HasDiscriminator<string>("EmployeeType")
            ///            .HasValue<FullTimeEmployee>("FTE")
            ///            .HasValue<PartTimeEmployee>("PTE");
            ///            

            //Map (TPT - Table Per Type) Approach
            //
            modelBuilder.Entity<FullTimeEmployee>() 
                        .ToTable("FullTimeEmployees");
            modelBuilder.Entity<PartTimeEmployee>()
                        .ToTable("PartTimeEmployees");
        }

    }
}
