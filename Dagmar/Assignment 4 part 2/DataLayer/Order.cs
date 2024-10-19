using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;

public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime Required { get; set; }
    public string ShipName { get; set; }
    public string ShipCity { get; set; }

    public virtual List<OrderDetails> OrderDetails { get; set; }

}

public class OrderDetails
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public double UnitPrice { get; set; }
    public double Quantity { get; set; }
    public double Discount { get; set; }

    public virtual Product Product { get; set; }
    public virtual Order Order { get; set; }

    public string ProductName 
    { 
        get
        {
            return Product.Name;
        }
    }

}
