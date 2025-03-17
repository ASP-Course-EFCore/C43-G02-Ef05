using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployeeDepartmentView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view FullTimeEmployeeView
                                        With encryption,schemaBinding
                                        As
                                         Select E.Name,E.Age,E.Address,FT.Salary,FT.StartDate
                                         From dbo.FullTimeEmployees FT join dbo.Employees E
                                         On FT.Id = E.Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Drop View FullTimeEmployeeView");
        }
    }
}
