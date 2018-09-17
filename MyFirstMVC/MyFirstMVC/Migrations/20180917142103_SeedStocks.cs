using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstMVC.Migrations
{
    public partial class SeedStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Склад 1" });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Склад 2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
