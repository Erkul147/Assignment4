using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;

internal class NorthwindContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=localhost;db=northwind;uid=postgres;pwd=moos");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        MapCategories(modelBuilder);
        MapProducts(modelBuilder);
        MapOrders(modelBuilder);
        MapOrderDetails(modelBuilder);


    }

    private static void MapCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
        modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
        modelBuilder.Entity<Category>().Property(x => x.Description).HasColumnName("description");

        // need a products (CHECK PAGE 432) 
        modelBuilder.Entity<Category>().HasMany(c => c.Products)
                                       .WithOne(p => p.Category)
                                       .HasForeignKey(p => p.CategoryId);

    }

    private static void MapProducts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnName("productid");
        modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnName("productname");
        modelBuilder.Entity<Product>().Property(x => x.CategoryId).HasColumnName("categoryid");
        modelBuilder.Entity<Product>().Property(x => x.QuantityPerUnit).HasColumnName("quantityperunit");
        modelBuilder.Entity<Product>().Property(x => x.UnitsInStock).HasColumnName("unitsinstock");
        modelBuilder.Entity<Product>().Property(x => x.UnitPrice).HasColumnName("unitprice");

        // need a category name (CHECK PAGE 432) 
        modelBuilder.Entity<Product>().HasOne(c => c.Category)
                                      .WithMany(c => c.Products)
                                      .HasForeignKey (p => p.CategoryId);

        // need a category name (CHECK PAGE 432) 
        modelBuilder.Entity<Product>().HasMany(c => c.OrderDetails)
                                    .WithOne(od => od.Product)
                                    .HasForeignKey(od => od.ProductId);


    }

    
   private static void MapOrders(ModelBuilder modelBuilder)
   {
        modelBuilder.Entity<Order>().ToTable("orders");
        modelBuilder.Entity<Order>().HasKey(x => x.Id);


        modelBuilder.Entity<Order>().Property(x => x.Id).HasColumnName("orderid");
        modelBuilder.Entity<Order>().Property(x => x.Date).HasColumnName("orderdate");
        modelBuilder.Entity<Order>().Property(x => x.Required).HasColumnName("requireddate");
        modelBuilder.Entity<Order>().Property(x => x.ShipName).HasColumnName("shipname");
        modelBuilder.Entity<Order>().Property(x => x.ShipCity).HasColumnName("shipcity");



        // need a category name (CHECK PAGE 432) 
        modelBuilder.Entity<Order>().HasMany(o => o.OrderDetails)
                                    .WithOne(o => o.Order)
                                    .HasForeignKey(d => d.OrderId);

    }


    
   private static void MapOrderDetails(ModelBuilder modelBuilder)
   {
        modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
        modelBuilder.Entity<OrderDetails>().HasKey(x => new { x.OrderId, x.ProductId });

        modelBuilder.Entity<OrderDetails>().Property(x => x.OrderId).HasColumnName("orderid");
        modelBuilder.Entity<OrderDetails>().Property(x => x.ProductId).HasColumnName("productid");
        modelBuilder.Entity<OrderDetails>().Property(x => x.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<OrderDetails>().Property(x => x.Quantity).HasColumnName("quantity");
        modelBuilder.Entity<OrderDetails>().Property(x => x.Discount).HasColumnName("discount");


        // ??
        //modelBuilder.Entity<OrderDetails>().Property(x => x.Product).HasColumnName("productname"); // Maybe join two tables? (orderdetails and product)
        modelBuilder.Entity<OrderDetails>().HasOne(d => d.Order)
                                           .WithMany(o => o.OrderDetails)
                                           .HasForeignKey(d => d.OrderId);

        modelBuilder.Entity<OrderDetails>().HasOne(p => p.Product)
                                           .WithMany(p => p.OrderDetails)
                                           .HasForeignKey(d => d.ProductId);

    }
    
    
}
