using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstMVC.Migrations
{
    public partial class AddStock3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Склад 3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
