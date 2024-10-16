
using DataLayer;

var DataService = new DataService();

foreach (var item in DataService.GetCategories())
{
    Console.WriteLine($"id = {item.Id}, Name = {item.Name}");
};



























