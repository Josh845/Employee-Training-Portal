using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee_Training_Portal.Migrations
{
    public partial class etpDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "employeeFK",
                table: "Progress",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employeeFK",
                table: "Progress");
        }
    }
}
