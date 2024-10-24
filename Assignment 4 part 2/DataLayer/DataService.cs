﻿
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class DataService : IDataService
{
    private NorthwindContext db;
    public DataService()
    {
        db = new NorthwindContext();
    }
    public Category CreateCategory(string name, string description)
    {
        int new_id = db.Categories.Max(x => x.Id) + 1; // increment largest id by one to create the new

        Category newCategory = new Category()
        {
            Id = new_id,
            Name = name,
            Description = description
        };

        db.Categories.Add(newCategory);

        db.SaveChanges();
        Console.WriteLine("new category gogogogo");

        return newCategory;
    }

    public bool UpdateCategory(int id, string name, string description)
    {
        var category = db.Categories.Find(id);

        if (category == null)
        {
            Console.WriteLine("is null");
            return false;
        }

        category.Name = name;
        category.Description = description;
        Console.WriteLine("go save changes");
        return db.SaveChanges() > 0;
    }
    public IList<Category>? GetCategories() // test 2
    {
        List<Category> categories = db.Categories.ToList();
        Console.WriteLine("get categories from db");
        if (categories == null)
        {
            return null;
        }
        return categories;
    }

    public Category? GetCategory(int id)
    {
        Category? category = db.Categories.Find(id);
        return category;
    }

    public bool DeleteCategory(int id)
    {
        Category? categoryToDelete = db.Categories.Find(id);
        if (categoryToDelete != null)  // if it is not null then we can add it to database
        { 
            db.Categories.Remove(categoryToDelete);
            return db.SaveChanges() > 0; // SaveChanges() returns 0 if no entries made to database
        } 

        return false;

    }

    public Product? GetProduct(int id)
    {
        try
        {
            var product = db.Products
                            .Include(p => p.Category)
                            .Where(p => p.Id == id)
                            .First();
            return product;

        } catch { }
        return null;
    }

    public IList<Product>? GetProductByCategory(int id)
    {
        IList<Product> products = db.Products
                                            .Include(p => p.Category)
                                            .Where(x => x.CategoryId == id)
                                            .ToList();
        return products;
    }

    public IList<Product> GetProductByName(string productName)
    {
        IList<Product> products = db.Products
                                        .Include(p => p.Category)
                                        .Where(x => x.Name.Contains(productName)).ToList();

       
        if (products == null || products.Count() == 0)
        {
            return new List<Product>();
        }

        return products;
    }

    public IList<Product> GetProducts()
    {
        List<Product> products = db.Products.ToList();
        return products;
    }


    public Order GetOrder(int id)
    {

        var order = db.Orders.Include(x => x.OrderDetails)
                             .ThenInclude(od => od.Product)
                             .ThenInclude(p => p.Category)
                             .FirstOrDefault(x => x.Id == id);

        if (order == null)
        {
            return null;
        }

        return order;
    }


    public IList<OrderDetails> GetOrderDetailsByOrderId(int id)
    {
        var order = db.Orders.Include(x => x.OrderDetails)
                             .ThenInclude(od => od.Product)
                             .ThenInclude(p => p.Category)
                             .FirstOrDefault(x => x.Id == id);
        if (order == null)
        {
            return null;
        }

        return order.OrderDetails;
    }

    public IList<OrderDetails> GetOrderDetailsByProductId(int id)
    {
        var product = db.Products.Include(x => x.OrderDetails)
                                 .ThenInclude(od => od.Order)  
                                 .Where(x => x.Id == id)
                                 .First();
        if (product == null)
        {
            return null;
        }

        return product.OrderDetails;
    }

    public IList<Order> GetOrders()
    {
        var orders = db.Orders.Include(x => x.OrderDetails)
                              .ThenInclude(od => od.Product)
                              .ThenInclude(p => p.Category)
                              .ToList();
        if (orders == null)
        {
            return null;
        }

        return orders;
    }




}