using DatabaseBenchmark.Models;

namespace DatabaseBenchmark.Helpers;

public static class GenerateCustomers
{
    public static List<MongoCustomer> GenerateRandomMongoCustomers(int count)
    {
        // Generate random customer data as BsonDocuments
        var customers = new List<MongoCustomer>();
        for (var i = 1; i <= count; i++)
            customers.Add(new MongoCustomer
            {
                Name = "Customer " + i,
                Email = "customer" + i + "@example.com",
                Location = new Location
                {
                    Address = "123 Main St.",
                    City = "Anytown",
                    State = "CA",
                    ZipCode = "12345"
                },
                Login = new Login
                {
                    Username = "user" + i,
                    Password = "password" + i
                },
                Pictures = new List<Picture>
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

    public static List<SqlServerCustomer> GenerateRandomSqlServerCustomers(int count)
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