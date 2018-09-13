using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyFirstMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<PhoneOnStock> PhonesOnStocks { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //fluent api
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Phone)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.PhoneId);

            modelBuilder.Entity<Phone>()
                .HasMany(p => p.Orders)
                .WithOne(p => p.Phone)
                .HasPrincipalKey(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Phone>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Phones)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Phones)
                .WithOne(c => c.Category)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhoneOnStock>()
                .HasKey(ps => new {ps.PhoneId, ps.StockId});

            modelBuilder.Entity<PhoneOnStock>()
                .HasOne(p => p.Phone)
                .WithMany(p => p.Stocks)
                .HasForeignKey(p => p.PhoneId);

            modelBuilder.Entity<PhoneOnStock>()
                .HasOne(p => p.Stock)
                .WithMany(p => p.Phones)
                .HasForeignKey(p => p.StockId);
        }
    }
}
