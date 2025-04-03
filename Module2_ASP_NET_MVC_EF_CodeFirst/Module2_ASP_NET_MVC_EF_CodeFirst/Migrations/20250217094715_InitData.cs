using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Module2_ASP_NET_MVC_EF_CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class InitData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("603dc73b-20cc-4b65-86cb-ba7e73955b69"), "Laptop", "Các sản phẩm laptop cao cấp" },
                    { new Guid("63ade4c0-9740-4ea8-a3d5-d22d892b38f1"), "Điện thoại", "Các sản phẩm điện thoại di động" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "CategoryId", "Price", "ProductName" },
                values: new object[,]
                {
                    { new Guid("b1ff595c-adf6-4534-9db4-c77e2a3aac2d"), new Guid("63ade4c0-9740-4ea8-a3d5-d22d892b38f1"), 24000000, "Samsung Galaxy S25 Ultra" },
                    { new Guid("dde5e481-d93b-412e-96a0-981f438ad338"), new Guid("603dc73b-20cc-4b65-86cb-ba7e73955b69"), 65000000, "Macbook Pro M4" },
                    { new Guid("e3fef37c-8b0d-4ac9-b984-262a7d92c825"), new Guid("63ade4c0-9740-4ea8-a3d5-d22d892b38f1"), 25000000, "IPhone 16 Pro" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: new Guid("b1ff595c-adf6-4534-9db4-c77e2a3aac2d"));

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: new Guid("dde5e481-d93b-412e-96a0-981f438ad338"));

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: new Guid("e3fef37c-8b0d-4ac9-b984-262a7d92c825"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("603dc73b-20cc-4b65-86cb-ba7e73955b69"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("63ade4c0-9740-4ea8-a3d5-d22d892b38f1"));
        }
    }
}
