using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MongoCustomer
{
    public BsonObjectId id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("location")]
    public Location Location { get; set; }

    [BsonElement("login")]
    public Login Login { get; set; }

    [BsonElement("pictures")]
    public List<Picture> Pictures { get; set; }
}

public class Location
{
    [BsonElement("address")]
    public string Address { get; set; }

    [BsonElement("city")]
    public string City { get; set; }

    [BsonElement("state")]
    public string State { get; set; }

    [BsonElement("zipcode")]
    public string ZipCode { get; set; }
}

public class Login
{
    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }
}

public class Picture
{
    [BsonElement("format")]
    public string Format { get; set; }

    [BsonElement("data")]
    public byte[] Data { get; set; }
}