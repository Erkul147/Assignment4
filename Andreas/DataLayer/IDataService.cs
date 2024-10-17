using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;

public interface IDataService
{
    // Category
    Category CreateCategory(string name, string description);
    IList<Category> GetCategories();

    Category GetCategory(int id);
    
    bool UpdateCategory(int id, string name, string description);

    bool DeleteCategory(int id);



    // Product
    Product GetProduct(int id);
    IList<Product> GetProducts();
    IList<Product> GetProductByCategory(int id);
    List<Product> GetProductByName(string name);


    // Orders
    Order GetOrder(int id);
    IList<Order> GetOrders();
    IList<OrderDetails> GetOrderDetailsByOrderId(int id);
    IList<OrderDetails> GetOrderDetailsByProductId(int id);

    


}
