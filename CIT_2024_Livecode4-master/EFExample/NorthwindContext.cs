using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExample;
internal class NorthwindContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=localhost;db=northwind;uid=bulskov;pwd=henrik");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
        modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
        modelBuilder.Entity<Category>().Property(x => x.Description).HasColumnName("description");
    }
}
