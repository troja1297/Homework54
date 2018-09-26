using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstMVC.Migrations
{
    public partial class AddCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Phones");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Phones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "IOS", null },
                    { 2, "Android", null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Apple" },
                    { 2, "Samsung" },
                    { 3, "Nokia" }
                });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "CategoryId", "CompanyId", "Name", "Price" },
                values: new object[] { 1, 1, 1, "Iphone 4", 200.0 });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "CategoryId", "CompanyId", "Name", "Price" },
                values: new object[] { 2, 2, 2, "Xperia", 100.0 });

            migrationBuilder.CreateIndex(
                name: "IX_Phones_CompanyId",
                table: "Phones",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Companies_CompanyId",
                table: "Phones",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Companies_CompanyId",
                table: "Phones");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Phones_CompanyId",
                table: "Phones");

            migrationBuilder.DeleteData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Phones");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Phones",
                nullable: true);
        }
    }
}
