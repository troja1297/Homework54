using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstMVC.Migrations
{
    public partial class UpdatePhone1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssemblyDate",
                value: new DateTime(2018, 8, 20, 12, 50, 0, 228, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssemblyDate",
                value: new DateTime(2018, 8, 20, 12, 50, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
