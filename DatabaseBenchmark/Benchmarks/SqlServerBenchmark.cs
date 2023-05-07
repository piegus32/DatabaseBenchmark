using BenchmarkDotNet.Attributes;
using DatabaseBenchmark.Models;

namespace DatabaseBenchmark.Benchmarks;

[MemoryDiagnoser]
[RankColumn]
public class SqlServerBenchmark
{
    public int Count { get; set; } = 1;

    private SqlServerDbContext dbContext;

    [GlobalSetup]
    public void GlobalSetup()
    {
        // SQL Server setup
        dbContext = new SqlServerDbContext();
        dbContext.Database.EnsureCreated();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        // SQL Server cleanup
        dbContext.Database.EnsureDeleted();
    }

    [Benchmark]
    public void SqlServer_Create()
    {
        var customers = GenerateRandomSqlServerCustomers(Count);
        dbContext.Customers.AddRange(customers);
        dbContext.SaveChanges();
    }

    [Benchmark]
    public void SqlServer_Read()
    {
        var customers = dbContext.Customers.ToList();
    }

    [Benchmark]
    public void SqlServer_Delete()
    {
        var customers = GenerateRandomSqlServerCustomers(Count);
        dbContext.RemoveRange(customers);
        dbContext.SaveChanges();
    }

    private static List<SqlServerCustomer> GenerateRandomSqlServerCustomers(int count)
    {
        var customers = new List<SqlServerCustomer>();
        for (var i = 1; i <= count; i++)
            customers.Add(new SqlServerCustomer
            {
                Name = "Customer " + i,
                Email = "customer" + i + "@example.com",
                Location = new Models.Location
                {
                    Address = "123 Main St.",
                    City = "Anytown",
                    State = "CA",
                    ZipCode = "12345"
                },
                Login = new Models.Login
                {
                    Username = "user" + i,
                    Password = "password" + i
                },
                Pictures = new List<Models.Picture>
                {
                    new()
                    {
                        Format = "JPEG",
                        Data = new byte[1024]
                    },
                    new()
                    {
                        Format = "PNG",
                        Data = new byte[2048]
                    }
                }
            });

        return customers;
    }
}