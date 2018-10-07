using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstMVC.Migrations
{
    public partial class AddPhones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Xiaomi" },
                    { 5, "Sony" }
                });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "AssemblyDate", "CategoryId", "CompanyId", "Name", "Price" },
                values: new object[,]
                {
                    { 4, new DateTime(1999, 8, 20, 12, 50, 0, 0, DateTimeKind.Unspecified), 2, 2, "Xperia Z", 100.0 },
                    { 7, new DateTime(2018, 8, 20, 12, 50, 0, 228, DateTimeKind.Unspecified), 1, 1, "Iphone X", 1200.0 }
                });

            migrationBuilder.UpdateData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CompanyId", "Name" },
                values: new object[] { 5, "Xperia Y" });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "AssemblyDate", "CategoryId", "CompanyId", "Name", "Price" },
                values: new object[] { 6, new DateTime(1999, 8, 20, 12, 50, 0, 0, DateTimeKind.Unspecified), 2, 4, "Mi mix 3", 300.0 });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "AssemblyDate", "CategoryId", "CompanyId", "Name", "Price" },
                values: new object[] { 3, new DateTime(1999, 8, 20, 12, 50, 0, 0, DateTimeKind.Unspecified), 2, 4, "Mi mix 2", 300.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CompanyId", "Name" },
                values: new object[] { 2, "Xperia" });
        }
    }
}
