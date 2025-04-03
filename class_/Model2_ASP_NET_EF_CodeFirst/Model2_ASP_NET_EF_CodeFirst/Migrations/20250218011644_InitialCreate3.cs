using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Model2_ASP_NET_EF_CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("8277c5d7-f4e3-4b42-b7c0-0ba29d42cf01"), "Clothing", "" },
                    { new Guid("a2a03748-c64d-472d-895d-be0d97486e4b"), "Home & Kitchen", "" },
                    { new Guid("cec14050-ae21-4f85-8488-744be8ad57fc"), "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Price", "ProductName" },
                values: new object[,]
                {
                    { new Guid("52814b42-95e3-45bf-b95c-cc4e47d33561"), new Guid("8277c5d7-f4e3-4b42-b7c0-0ba29d42cf01"), 20, "T-Shirt" },
                    { new Guid("9795f5f0-64d3-4cc7-ac11-28eac50f5e6f"), new Guid("cec14050-ae21-4f85-8488-744be8ad57fc"), 1000, "Laptop" },
                    { new Guid("b280c07d-6060-4358-971b-631badddd906"), new Guid("a2a03748-c64d-472d-895d-be0d97486e4b"), 50, "Blender" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("52814b42-95e3-45bf-b95c-cc4e47d33561"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("9795f5f0-64d3-4cc7-ac11-28eac50f5e6f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("b280c07d-6060-4358-971b-631badddd906"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("8277c5d7-f4e3-4b42-b7c0-0ba29d42cf01"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("a2a03748-c64d-472d-895d-be0d97486e4b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("cec14050-ae21-4f85-8488-744be8ad57fc"));
        }
    }
}
