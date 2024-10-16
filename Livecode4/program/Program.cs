// Npgsql.EntityFrameworkCore.Po


using program;

var context = new northwindContext();
var categories = context.Categories.Where(x => x.Id < 5).ToList();

foreach(var e in categories)
{
    Console.WriteLine(e.Name);

}















