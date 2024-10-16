
namespace DataLayer;

public class DataService : IDataService
{
    private NorthwindContext db;

    public DataService()
    {
        db = new NorthwindContext();

    }

    public int CreateCategory(string name, string description)
    {
        throw new NotImplementedException();
    }

    public bool DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }

    public IList<Category> GetCategories()
    {
        return db.Categories.ToList();
    }

    public Category GetCategory(int id)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public Category UpdateCategory(int id, string name, string description)
    {
        throw new NotImplementedException();
    }
}