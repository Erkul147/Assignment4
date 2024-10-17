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
    IList<Product> GetProducts();
    Product GetProduct(int id);
    Product GetProductByCategory(int id);
    Product GetProductByName(string name);


    // Orders
    Order GetOrder(int id);
    IList<Order> GetOrders();
    OrderDetails GetOrderDetailsByOrderId(int id);
    OrderDetails GetOrderDetailsByProductId(int id);

    


}
