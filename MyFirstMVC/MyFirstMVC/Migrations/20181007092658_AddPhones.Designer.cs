﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyFirstMVC.Models;

namespace MyFirstMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181007092658_AddPhones")]
    partial class AddPhones
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyFirstMVC.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("ParentCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new { Id = 1, Name = "IOS" },
                        new { Id = 2, Name = "Android" }
                    );
                });

            modelBuilder.Entity("MyFirstMVC.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new { Id = 1, Name = "Apple" },
                        new { Id = 2, Name = "Samsung" },
                        new { Id = 3, Name = "Nokia" },
                        new { Id = 4, Name = "Xiaomi" },
                        new { Id = 5, Name = "Sony" }
                    );
                });

            modelBuilder.Entity("MyFirstMVC.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Message");

                    b.Property<int>("PhoneId");

                    b.Property<int>("Rating");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("MyFirstMVC.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Addres");

                    b.Property<string>("ContactPhone");

                    b.Property<int>("PhoneId");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MyFirstMVC.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AssemblyDate");

                    b.Property<int>("CategoryId");

                    b.Property<int>("CompanyId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Phones");

                    b.HasData(
                        new { Id = 1, AssemblyDate = new DateTime(2018, 8, 20, 12, 50, 0, 228, DateTimeKind.Unspecified), CategoryId = 1, CompanyId = 1, Name = "Iphone 4", Price = 200.0 },
                        new { Id = 2, AssemblyDate = new DateTime(1999, 8, 20, 12, 50, 0, 0, DateTimeKind.Unspecified), CategoryId = 2, CompanyId = 5, Name = "Xperia Y", Price = 100.0 },
                        new { Id = 3, AssemblyDate = new DateTime(1999, 8, 20, 12, 50, 0, 0, DateTimeKind.Unspecified), CategoryId = 2, CompanyId = 4, Name = "Mi mix 2", Price = 300.0 },
                        new { Id = 4, AssemblyDate = new DateTime(1999, 8, 20, 12, 50, 0, 0, DateTimeKind.Unspecified), CategoryId = 2, CompanyId = 2, Name = "Xperia Z", Price = 100.0 },
                        new { Id = 6, AssemblyDate = new DateTime(1999, 8, 20, 12, 50, 0, 0, DateTimeKind.Unspecified), CategoryId = 2, CompanyId = 4, Name = "Mi mix 3", Price = 300.0 },
                        new { Id = 7, AssemblyDate = new DateTime(2018, 8, 20, 12, 50, 0, 228, DateTimeKind.Unspecified), CategoryId = 1, CompanyId = 1, Name = "Iphone X", Price = 1200.0 }
                    );
                });

            modelBuilder.Entity("MyFirstMVC.Models.PhoneOnStock", b =>
                {
                    b.Property<int>("PhoneId");

                    b.Property<int>("StockId");

                    b.Property<int>("Quantity");

                    b.HasKey("PhoneId", "StockId");

                    b.HasIndex("StockId");

                    b.ToTable("PhonesOnStocks");
                });

            modelBuilder.Entity("MyFirstMVC.Models.Shaurma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<double>("Price");

                    b.Property<int>("ShaurmaSize");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Shaurmas");
                });

            modelBuilder.Entity("MyFirstMVC.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Stocks");

                    b.HasData(
                        new { Id = 1, Name = "Склад 1" },
                        new { Id = 2, Name = "Склад 2" },
                        new { Id = 3, Name = "Склад 3" }
                    );
                });

            modelBuilder.Entity("MyFirstMVC.Models.Category", b =>
                {
                    b.HasOne("MyFirstMVC.Models.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyFirstMVC.Models.Feedback", b =>
                {
                    b.HasOne("MyFirstMVC.Models.Phone", "Phone")
                        .WithMany("Feedbacks")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyFirstMVC.Models.Order", b =>
                {
                    b.HasOne("MyFirstMVC.Models.Phone", "Phone")
                        .WithMany("Orders")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyFirstMVC.Models.Phone", b =>
                {
                    b.HasOne("MyFirstMVC.Models.Category", "Category")
                        .WithMany("Phones")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MyFirstMVC.Models.Company", "Company")
                        .WithMany("Phones")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyFirstMVC.Models.PhoneOnStock", b =>
                {
                    b.HasOne("MyFirstMVC.Models.Phone", "Phone")
                        .WithMany("PhoneOnStocks")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyFirstMVC.Models.Stock", "Stock")
                        .WithMany("PhoneOnStocks")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyFirstMVC.Models.Shaurma", b =>
                {
                    b.HasOne("MyFirstMVC.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
