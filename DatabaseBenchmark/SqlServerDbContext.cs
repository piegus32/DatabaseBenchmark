using DatabaseBenchmark.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBenchmark;

public class SqlServerDbContext : DbContext
{
    public DbSet<SqlServerCustomer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Server=DESKTOP-V6RGKJO;Initial Catalog=Test;User ID=ServiceApi;Password=Miszczu16;Encrypt=False");
        optionsBuilder.UseSqlServer("Server=127.0.0.1;Initial Catalog=Test;User ID=SA;Password=Miszczu16;Encrypt=False");
    }
}