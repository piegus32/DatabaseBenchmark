using BenchmarkDotNet.Attributes;
using DatabaseBenchmark.Helpers;
using DatabaseBenchmark.Models;
using MongoDB.Driver;

namespace DatabaseBenchmark.Benchmarks;

[JsonExporterAttribute.Full()]
[RPlotExporter]
[MemoryDiagnoser]
[WarmupCount(5)]
[IterationCount(15)]
public class InsertCustomersBenchmark
{
    private MongoClient? _client;
    private IMongoCollection<MongoCustomer> _collection = null!;
    private IMongoDatabase _database = null!;
    private List<MongoCustomer> _noSqlCustomers;
    private List<SqlServerCustomer> _sqlCustomers;
    private SqlServerDbContext dbContext;

    [Params(100)] public int Count { get; set; }

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

    [IterationSetup]
    public void IterationSetup()
    {
        dbContext = new SqlServerDbContext();
        dbContext.Database.EnsureCreated();
        _database = _client.GetDatabase("testdb");
        _collection = _database.GetCollection<MongoCustomer>("customers");
        _sqlCustomers = GenerateCustomers.GenerateRandomSqlServerCustomers(Count);
        _noSqlCustomers = GenerateCustomers.GenerateRandomMongoCustomers(Count);
    }

    [IterationCleanup]
    public void IterationCleanup()
    {
        //_sqlCustomers.Clear();
        //_noSqlCustomers.Clear();
        //dbContext.Customers.RemoveRange(dbContext.Customers.ToList());
        //dbContext.SaveChanges();
        //_collection.DeleteMany(_ => true);

        _sqlCustomers.Clear();
        _noSqlCustomers.Clear();
        // drop the customers collection
        _database.DropCollection("customers");
        // SQL Server cleanup
        dbContext.Database.EnsureDeleted();
    }

    [Benchmark]
    public void InsertSqlCustomer()
    {
        dbContext.Customers.AddRange(_sqlCustomers);
        dbContext.SaveChanges();
    }

    [Benchmark]
    public void InsertNoSqlCustomers()
    {
        _collection.InsertMany(_noSqlCustomers);
    }
}