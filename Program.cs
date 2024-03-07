using AzureProject;

internal class Program
{
    private static async Task Main(string[] args)
    {
        
        // Get the current directory
        // Define the relative file path
        string relativeFilePath = Path.Combine("..", "..", "..", "resources", "data.json");

        // Read the JSON data from the file
        string jsonData = File.ReadAllText(relativeFilePath);

        // Parse JSON string into Person object
        Person person = Person.FromJson(jsonData);

        // Access properties of the person object
        Console.WriteLine($"First Name: {person.FirstName}");
        Console.WriteLine($"Last Name: {person.LastName}");
        Console.WriteLine($"Age: {person.Age}");
        Console.WriteLine($"Email: {person.Email}");
        
        
        
        // var connectionString = "your_connection_string";
        // var folderPath = "path_to_your_folder";
        // var containerName = "your_container_name";
        // var blobName = "your_blob_name";
        //
        // var storageManager = new BlobStorageManager(connectionString);
        // await storageManager.UploadFolderAsZipAsync(folderPath, containerName, blobName);
        //
        // Console.WriteLine("Folder uploaded as zip to Azure Blob Storage.");
    }
}