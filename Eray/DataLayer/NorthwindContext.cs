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
    //public DbSet<Order> Orders { get; set; }
    //public DbSet<OrderDetails> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=localhost;db=Northwind;uid=postgres;pwd=Say67krk#");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        MapCategories(modelBuilder);
        MapProducts(modelBuilder);
        //MapOrders(modelBuilder);
        //MapOrderDetails(modelBuilder);


    }

    private static void MapCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
        modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
        modelBuilder.Entity<Category>().Property(x => x.Description).HasColumnName("description");

    }

    private static void MapProducts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnName("productid");
        modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnName("productname");
        modelBuilder.Entity<Product>().Property(x => x.CategoryId).HasColumnName("categoryid");
        modelBuilder.Entity<Product>().Property(x => x.CategoryId).HasColumnName("categoryid");
        //modelBuilder.Entity<Product>().Property(x => x.Category).HasColumnName("category");
        modelBuilder.Entity<Product>().Property(x => x.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<Product>().Property(x => x.QuantityPerUnit).HasColumnName("quantityperunit");
        modelBuilder.Entity<Product>().Property(x => x.UnitsInStock).HasColumnName("unitinstock");


        //modelBuilder.Entity<Product>().Property(x => x.CategoryName).HasColumnName("");




    }
    /*
   private static void MapOrders(ModelBuilder modelBuilder)
   {
        modelBuilder.Entity<Order>().ToTable("orders");
        modelBuilder.Entity<Order>().Property(x => x.Id).HasColumnName("orderid");
        modelBuilder.Entity<Order>().Property(x => x.Date).HasColumnName("orderdate");
        modelBuilder.Entity<Order>().Property(x => x.Required).HasColumnName("requireddate");
        modelBuilder.Entity<Order>().Property(x => x.ShipName).HasColumnName("shipname");
        modelBuilder.Entity<Order>().Property(x => x.ShipCity).HasColumnName("shipcity");

        modelBuilder.Entity<Order>().Property(x => x.OrderDetails).HasColumnName("orderdetails"); // Make a list of order and orderdetails and zip them


    }



    private static void MapOrderDetails(ModelBuilder modelBuilder)
   {
        modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
        modelBuilder.Entity<OrderDetails>().Property(x => x.OrderId).HasColumnName("orderid");
        modelBuilder.Entity<OrderDetails>().Property(x => x.ProductId).HasColumnName("productid");
        modelBuilder.Entity<OrderDetails>().Property(x => x.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<OrderDetails>().Property(x => x.Quantity).HasColumnName("quanity");
        modelBuilder.Entity<OrderDetails>().Property(x => x.Discount).HasColumnName("discount");

        modelBuilder.Entity<OrderDetails>().Property(x => x.Product).HasColumnName("productname"); // Maybe join two tables? (orderdetails and product)

        modelBuilder.Entity<OrderDetails>().Property(x => x.Order).HasColumnName("order"); // Make a list of order and orderdetails and zip them



    }
    */
}
