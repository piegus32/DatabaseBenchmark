using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters.Csv;
using DatabaseBenchmark.Helpers;
using DatabaseBenchmark.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace DatabaseBenchmark.Benchmarks;

[JsonExporterAttribute.Full()]
[RPlotExporter]
[MemoryDiagnoser]
public class ReadCustomersBenchmark
{
    private MongoClient? _client;
    private SqlServerDbContext dbContext;
    private IMongoCollection<MongoCustomer> _collection = null!;
    private IMongoDatabase _database = null!;

    [Params(100, 1000, 10000, 50000, 100000)] public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        // connect to the MongoDB server and database
        _client = new MongoClient("mongodb://localhost:27017");
        _database = _client.GetDatabase("testdb");
        _collection = _database.GetCollection<MongoCustomer>("customers");

        // SQL Server setup
        dbContext = new SqlServerDbContext();
        dbContext.Database.EnsureCreated();

        //Insert objects
        dbContext.AddRange(GenerateCustomers.GenerateRandomSqlServerCustomers(Count));
        dbContext.SaveChanges();
        _collection.InsertMany(GenerateCustomers.GenerateRandomMongoCustomers(Count));
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        // drop the customers collection
        _database.DropCollection("customers");
        // SQL Server cleanup
        dbContext.Database.EnsureDeleted();
        // disconnect from servers
        _client = null;
        dbContext.Dispose();
    }

    [Benchmark]
    public List<MongoCustomer> ReadMongoCustomers()
    {
        var result = _collection.Find(_ => true).ToList();
        return result;
    }

    [Benchmark]
    public List<SqlServerCustomer> ReadSqlCustomers()
    {
        var customers = dbContext.Customers
            .Include(user => user.Location)
            .Include(user => user.Login)
            .Include(user => user.Pictures)
            .ToList();

        return customers;
    }
}