﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Module2_ASP_NET_MVC_EF_CodeFirst.Models;

#nullable disable

namespace Module2_ASP_NET_MVC_EF_CodeFirst.Migrations
{
    [DbContext(typeof(PRN222DBContext))]
    [Migration("20250218022034_InitialCreate4")]
    partial class InitialCreate4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Module2_ASP_NET_MVC_EF_CodeFirst.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("categories");

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("4db60c48-e979-4466-9fc6-79418684022e"),
                            CategoryName = "Điện thoại",
                            Description = "Các sản phẩm điện thoại di động"
                        },
                        new
                        {
                            CategoryId = new Guid("0cbbda2b-75ab-4d2e-999b-b9fc9633b5ec"),
                            CategoryName = "Laptop",
                            Description = "Các sản phẩm laptop cao cấp"
                        });
                });

            modelBuilder.Entity("Module2_ASP_NET_MVC_EF_CodeFirst.Models.Order", b =>
                {
                    b.Property<Guid>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShipAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderID");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("Module2_ASP_NET_MVC_EF_CodeFirst.Models.OrderDetail", b =>
                {
                    b.Property<Guid>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("orderDetails");
                });

            modelBuilder.Entity("Module2_ASP_NET_MVC_EF_CodeFirst.Models.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("products");

                    b.HasData(
                        new
                        {
                            ProductId = new Guid("bc10cf73-e235-49ff-be51-72ba8238b9e3"),
                            CategoryId = new Guid("4db60c48-e979-4466-9fc6-79418684022e"),
                            Price = 25000000,
                            ProductName = "IPhone 16 Pro"
                        },
                        new
                        {
                            ProductId = new Guid("4a987f99-8d4b-446d-9610-a5198943eaf2"),
                            CategoryId = new Guid("4db60c48-e979-4466-9fc6-79418684022e"),
                            Price = 24000000,
                            ProductName = "Samsung Galaxy S25 Ultra"
                        },
                        new
                        {
                            ProductId = new Guid("3a003a73-86a4-471b-9a15-c6e28e51d231"),
                            CategoryId = new Guid("0cbbda2b-75ab-4d2e-999b-b9fc9633b5ec"),
                            Price = 65000000,
                            ProductName = "Macbook Pro M4"
                        });
                });

            modelBuilder.Entity("Module2_ASP_NET_MVC_EF_CodeFirst.Models.OrderDetail", b =>
                {
                    b.HasOne("Module2_ASP_NET_MVC_EF_CodeFirst.Models.Order", "Order")
                        .WithMany("Orderdetail")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Module2_ASP_NET_MVC_EF_CodeFirst.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Module2_ASP_NET_MVC_EF_CodeFirst.Models.Product", b =>
                {
                    b.HasOne("Module2_ASP_NET_MVC_EF_CodeFirst.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Module2_ASP_NET_MVC_EF_CodeFirst.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Module2_ASP_NET_MVC_EF_CodeFirst.Models.Order", b =>
                {
                    b.Navigation("Orderdetail");
                });
#pragma warning restore 612, 618
        }
    }
}
