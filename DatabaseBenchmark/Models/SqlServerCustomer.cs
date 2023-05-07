using System.ComponentModel.DataAnnotations;

namespace DatabaseBenchmark.Models;

public class SqlServerCustomer
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Location Location { get; set; }
    public Login Login { get; set; }
    public List<Picture> Pictures { get; set; }
}

public class Location
{
    [Key]
    public int Id { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}

public class Login
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class Picture
{
    [Key]
    public int Id { get; set; }
    public string Format { get; set; }
    public byte[] Data { get; set; }

    public SqlServerCustomer Customer { get; set; }
}
