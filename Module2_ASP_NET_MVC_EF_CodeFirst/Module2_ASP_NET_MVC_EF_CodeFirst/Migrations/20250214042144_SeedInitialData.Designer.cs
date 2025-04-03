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
    [Migration("20250214042144_SeedInitialData")]
    partial class SeedInitialData
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
                            CategoryId = new Guid("33ea8497-1a66-4915-a1c2-a6f5c1469771"),
                            CategoryName = "Điện thoại",
                            Description = "Các sản phẩm điện thoại di động"
                        },
                        new
                        {
                            CategoryId = new Guid("bb94ba06-e428-49a7-b9ad-3f4d4f7cded3"),
                            CategoryName = "Laptop",
                            Description = "Các sản phẩm laptop cao cấp"
                        });
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
                            ProductId = new Guid("84bfc921-c42b-4c4b-b580-bdc0b4ce46e6"),
                            CategoryId = new Guid("33ea8497-1a66-4915-a1c2-a6f5c1469771"),
                            Price = 25000000,
                            ProductName = "IPhone 16 Pro"
                        },
                        new
                        {
                            ProductId = new Guid("17a666ad-b7f9-469a-a35b-496caf52e9dc"),
                            CategoryId = new Guid("33ea8497-1a66-4915-a1c2-a6f5c1469771"),
                            Price = 24000000,
                            ProductName = "Samsung Galaxy S25 Ultra"
                        },
                        new
                        {
                            ProductId = new Guid("7162fdc2-4595-419f-b213-86b48cfed96d"),
                            CategoryId = new Guid("bb94ba06-e428-49a7-b9ad-3f4d4f7cded3"),
                            Price = 65000000,
                            ProductName = "Macbook Pro M4"
                        });
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
#pragma warning restore 612, 618
        }
    }
}
