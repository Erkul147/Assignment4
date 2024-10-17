
using DataLayer;

// to try out the implementation before running tests
var dataService = new DataService();
var order = dataService.GetOrder(10248);

Console.WriteLine("orderdetails: " + order.OrderDetails.Count);

foreach (var item in order.OrderDetails)
{
    Console.WriteLine("run");
    Console.WriteLine(item.Product.Name + "    "
        + item.Product.Category.Name);

}



/*var newCat = dataService.CreateCategory("bob", "test");

PrintCategories(dataService);

dataService.UpdateCategory(newCat.Id, "bob", "test2");

PrintCategories(dataService);

dataService.DeleteCategory(newCat.Id);*/



//PrintProducts(dataService);









static void PrintCategories(DataService dataService)
{
    IList<Category> categories = dataService.GetCategories();

    foreach (var category in categories)
    {
        Console.WriteLine($"Id: {category.Id}, Name: {category.Name}, Desc: {category.Description}");
    }
}

static void PrintProducts(DataService dataService)
{
    IList<Product> categories = dataService.GetProducts();

    foreach (var product in categories)
    {
        Console.WriteLine($"Id: {product.Id}, Name: {product.Name}");
    }
}
















