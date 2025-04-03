﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Module2_ASP_NET_MVC_EF_CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderAndOrderDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: new Guid("17a666ad-b7f9-469a-a35b-496caf52e9dc"));

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: new Guid("7162fdc2-4595-419f-b213-86b48cfed96d"));

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: new Guid("84bfc921-c42b-4c4b-b580-bdc0b4ce46e6"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("33ea8497-1a66-4915-a1c2-a6f5c1469771"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("bb94ba06-e428-49a7-b9ad-3f4d4f7cded3"));

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShipAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("33ea8497-1a66-4915-a1c2-a6f5c1469771"), "Điện thoại", "Các sản phẩm điện thoại di động" },
                    { new Guid("bb94ba06-e428-49a7-b9ad-3f4d4f7cded3"), "Laptop", "Các sản phẩm laptop cao cấp" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "CategoryId", "Price", "ProductName" },
                values: new object[,]
                {
                    { new Guid("17a666ad-b7f9-469a-a35b-496caf52e9dc"), new Guid("33ea8497-1a66-4915-a1c2-a6f5c1469771"), 24000000, "Samsung Galaxy S25 Ultra" },
                    { new Guid("7162fdc2-4595-419f-b213-86b48cfed96d"), new Guid("bb94ba06-e428-49a7-b9ad-3f4d4f7cded3"), 65000000, "Macbook Pro M4" },
                    { new Guid("84bfc921-c42b-4c4b-b580-bdc0b4ce46e6"), new Guid("33ea8497-1a66-4915-a1c2-a6f5c1469771"), 25000000, "IPhone 16 Pro" }
                });
        }
    }
}
