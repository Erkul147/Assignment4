
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
        throw new NotImplementedException();
    }

    public OrderDetails GetOrderDetailsByOrderId(int id)
    {
        throw new NotImplementedException();
    }

    public OrderDetails GetOrderDetailsByProductId(int id)
    {
        throw new NotImplementedException();
    }

    public IList<Order> GetOrders()
    {
        throw new NotImplementedException();
    }

    public Product GetProduct(int id)
    {
        var product = db.Products.Find(id);

        if (product != null)
        {
            return product;
        }
        return null;

    }

    public Product GetProductByCategory(int id)
    {
        throw new NotImplementedException();
    }

    public Product GetProductByName(string name)
    {
        throw new NotImplementedException();
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