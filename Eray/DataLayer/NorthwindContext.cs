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
        optionsBuilder.UseNpgsql("host=localhost;db=Northwind;uid=postgres;pwd=Say67krk#");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Creating these models
        MapCategories(modelBuilder);
        MapProducts(modelBuilder);
        MapOrders(modelBuilder);
        MapOrderDetails(modelBuilder);


    }

    private static void MapCategories(ModelBuilder modelBuilder)
    {
        // Configuring how the "category" entity is set up in the database
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
        modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
        modelBuilder.Entity<Category>().Property(x => x.Description).HasColumnName("description");

    }

    private static void MapProducts(ModelBuilder modelBuilder)
    {
        // configuring how the "Product" entity is set up in the database
        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnName("productid");
        modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnName("productname");
        modelBuilder.Entity<Product>().Property(x => x.ProductName).HasColumnName("productname");
        modelBuilder.Entity<Product>().Property(x => x.CategoryId).HasColumnName("categoryid");
        // modelBuilder.Entity<Product>().Property(x => x.Category).HasColumnName("category");
        modelBuilder.Entity<Product>().Property(x => x.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<Product>().Property(x => x.QuantityPerUnit).HasColumnName("quantityperunit");
        modelBuilder.Entity<Product>().Property(x => x.UnitsInStock).HasColumnName("unitsinstock");

        // Relations.
        modelBuilder.Entity<Product>().HasOne(x => x.Category)  // Each product is related to one category. In other words every product belongs to one specific category.
                                      .WithMany(x => x.Products)  //Each category can have many products. So one category can contain multiple products.
                                      .HasForeignKey(x => x.CategoryId); // Joins Category and Products at CategoryId.


    }

    private static void MapOrders(ModelBuilder modelBuilder)
    {
        // configuring how the "Order" entity is set up in the database
        modelBuilder.Entity<Order>().ToTable("orders");
        modelBuilder.Entity<Order>().Property(x => x.Id).HasColumnName("orderid");
        modelBuilder.Entity<Order>().Property(x => x.Date).HasColumnName("orderdate");
        modelBuilder.Entity<Order>().Property(x => x.Required).HasColumnName("requireddate");
        modelBuilder.Entity<Order>().Property(x => x.ShipName).HasColumnName("shipname");
        modelBuilder.Entity<Order>().Property(x => x.ShipCity).HasColumnName("shipcity");

        // Keys.
        modelBuilder.Entity<Order>().HasKey(x => x.Id); // set up a unique identifier for the Order entity


    }

    private static void MapOrderDetails(ModelBuilder modelBuilder)
    {
        // configuring how the "OrderDetails" entity is set up in the database
        modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
        modelBuilder.Entity<OrderDetails>().Property(x => x.OrderId).HasColumnName("orderid");
        modelBuilder.Entity<OrderDetails>().Property(x => x.ProductId).HasColumnName("productid");
        modelBuilder.Entity<OrderDetails>().Property(x => x.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<OrderDetails>().Property(x => x.Quantity).HasColumnName("quantity");
        modelBuilder.Entity<OrderDetails>().Property(x => x.Discount).HasColumnName("discount");
        
        // Composite keys.
        modelBuilder.Entity<OrderDetails>().HasKey(x => new { x.OrderId, x.ProductId });  // Set up a unique identifier for the OrderDetails entity 
                                                                                          // Used an anonymouds type, because it is a way to combines these two properties into one

        // Relations
        modelBuilder.Entity<OrderDetails>()
            .HasOne(x => x.Order) // Each OrderDetails is related to one specific Order. So every OrderDetails belongs to one order.
            .WithMany(x => x.OrderDetails) // One Order can have multiple OrderDetails.
            .HasForeignKey(x => x.OrderId);  // Joins Order and OrderDetails at OrderId.

        modelBuilder.Entity<OrderDetails>() // Product and OrderDetails share the same relationship structure as Order and OrderDetails
            .HasOne(x => x.Product)  
            .WithMany(x => x.OrderDetails)  
            .HasForeignKey(x => x.ProductId);  
    }


}
