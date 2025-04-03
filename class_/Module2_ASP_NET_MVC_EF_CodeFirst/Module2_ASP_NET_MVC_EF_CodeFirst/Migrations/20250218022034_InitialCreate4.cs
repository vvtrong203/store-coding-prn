using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Module2_ASP_NET_MVC_EF_CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: new Guid("a0530e0f-113e-42ce-86d3-2269872f85cb"));

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: new Guid("e72ca120-6fd3-4e7c-81a5-d15ef5f93495"));

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: new Guid("fa305e9c-f59d-4aa2-87ca-72aea70fe8c0"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("02e3645c-801f-48bf-af73-73cb5218c135"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("8ba7a524-5574-48e0-94bf-ab8cfe7aaaa0"));

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("0cbbda2b-75ab-4d2e-999b-b9fc9633b5ec"), "Laptop", "Các sản phẩm laptop cao cấp" },
                    { new Guid("4db60c48-e979-4466-9fc6-79418684022e"), "Điện thoại", "Các sản phẩm điện thoại di động" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "CategoryId", "Price", "ProductName" },
                values: new object[,]
                {
                    { new Guid("3a003a73-86a4-471b-9a15-c6e28e51d231"), new Guid("0cbbda2b-75ab-4d2e-999b-b9fc9633b5ec"), 65000000, "Macbook Pro M4" },
                    { new Guid("4a987f99-8d4b-446d-9610-a5198943eaf2"), new Guid("4db60c48-e979-4466-9fc6-79418684022e"), 24000000, "Samsung Galaxy S25 Ultra" },
                    { new Guid("bc10cf73-e235-49ff-be51-72ba8238b9e3"), new Guid("4db60c48-e979-4466-9fc6-79418684022e"), 25000000, "IPhone 16 Pro" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: new Guid("3a003a73-86a4-471b-9a15-c6e28e51d231"));

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: new Guid("4a987f99-8d4b-446d-9610-a5198943eaf2"));

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: new Guid("bc10cf73-e235-49ff-be51-72ba8238b9e3"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("0cbbda2b-75ab-4d2e-999b-b9fc9633b5ec"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("4db60c48-e979-4466-9fc6-79418684022e"));

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("02e3645c-801f-48bf-af73-73cb5218c135"), "Laptop", "Các sản phẩm laptop cao cấp" },
                    { new Guid("8ba7a524-5574-48e0-94bf-ab8cfe7aaaa0"), "Điện thoại", "Các sản phẩm điện thoại di động" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "CategoryId", "Price", "ProductName" },
                values: new object[,]
                {
                    { new Guid("a0530e0f-113e-42ce-86d3-2269872f85cb"), new Guid("02e3645c-801f-48bf-af73-73cb5218c135"), 65000000, "Macbook Pro M4" },
                    { new Guid("e72ca120-6fd3-4e7c-81a5-d15ef5f93495"), new Guid("8ba7a524-5574-48e0-94bf-ab8cfe7aaaa0"), 25000000, "IPhone 16 Pro" },
                    { new Guid("fa305e9c-f59d-4aa2-87ca-72aea70fe8c0"), new Guid("8ba7a524-5574-48e0-94bf-ab8cfe7aaaa0"), 24000000, "Samsung Galaxy S25 Ultra" }
                });
        }
    }
}
