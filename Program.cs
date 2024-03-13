using dotenv.net;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static async Task Main(string[] args)
    {
        DotEnv.Load();

        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        var containerName = Environment.GetEnvironmentVariable("CONTAINER_NAME");
        var filePath =
            @"C:\Users\atash\RiderProjects\AzureProject\resources\Test_file\New Microsoft Publisher Document.pub";

        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            // Add other logging providers as needed
        });
        var logger = loggerFactory.CreateLogger<BlobStorageUploader>();

        var uploader = new BlobStorageUploader(connectionString, containerName, logger);
        await uploader.UploadFileAsync(filePath);


        // Path to the directory you want to upload
        var directoryPath = @"C:\Users\atash\RiderProjects\AzureProject\resources\Test_dir";
        ;

        try
        {
            // Upload the directory to Azure Blob Storage
            await uploader.UploadDirectoryAsync(directoryPath);
            Console.WriteLine("Directory uploaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}