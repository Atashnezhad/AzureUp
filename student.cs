using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AzureProject;

public class Person
{
    // Constructor for deserialization
    // the name in the json file is firstname
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    [JsonPropertyName("age")]
    public int Age { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }

    // Method to parse JSON string into Person object
    public static Person FromJson(string json)
    {
        return JsonSerializer.Deserialize<Person>(json);
    }
}