
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata.Ecma335;

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
        int id = db.Categories.Max(x => x.Id) + 1; // Initilizing a new element (ID)
        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };

        db.Categories.Add(category);

        db.SaveChanges();

        return category;

    }

    public bool DeleteCategory(int id)
    {
        var category = db.Categories.Find(id);

        if (category == null)
        {
            return false;
        }

        db.Categories.Remove(category);

        return db.SaveChanges() > 0; //
    }

    public IList<Category> GetCategories()
    {
        return db.Categories.ToList();
    }

    public Category GetCategory(int id)
    {
        var category = db.Categories.Find(id);

        if (category != null)
        {
            return category; 
        }
        return null;

    }


    public Order GetOrder(int id)
    {
        var order = db.Orders
                    .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Category)
                    .FirstOrDefault(x => x.Id == id);
        
        if(order == null)
        {
            return null;

        }
        
        return order;
    }

    public IList<OrderDetails> GetOrderDetailsByOrderId(int id)
    {
        var orderdetail = db.OrderDetails.Include(x => x.Product)
                                         .Where(x => x.OrderId == id)
                                         .ToList();
        return orderdetail;

    }

    public IList<OrderDetails> GetOrderDetailsByProductId(int id)
    {
        var orderdetail = db.Products.Include(x => x.OrderDetails)
                                     .ThenInclude(x => x.Order)
                                     .Where(x => x.Id == id)
                                     .FirstOrDefault();
        return orderdetail.OrderDetails;

    }

    public IList<Order> GetOrders()
    {
        return db.Orders.ToList();

    }

    public Product GetProduct(int id)
    {
        var product = db.Products
            .Include(x => x.Category) 
            .FirstOrDefault(x => x.Id == id);

        return product;

    }

    public IList<Product> GetProductByCategory(int id)
    {
        return db.Products.Include(x => x.Category)
                          .Where(x => x.CategoryId == id)
                          .ToList();
    }

    public IList<Product> GetProductByName(string name)
    {
        return db.Products.Include(x => x.Category)
                          .Where(x => x.ProductName.Contains(name))
                          .ToList();
    }

    public IList<Product> GetProducts()
    {
        return db.Products.Include(x => x.Category).ToList();
    }

    public bool UpdateCategory(int id, string name, string description)
    {
        var DbCategory = db.Categories.Find(id);

        if (DbCategory != null)
        {
            DbCategory.Id = id;
            DbCategory.Name = name;
            DbCategory.Description = description;
            return db.SaveChanges() > 0;

        }
        return false;
        



    }
}