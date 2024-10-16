// See https://aka.ms/new-console-template for more information
using EFExample;



var context = new NorthwindContext();
var categories = context.Categories.Where(x => x.Id < 5).ToList();

foreach(var e in categories)
{
    Console.WriteLine(e.Name);
}
