using dotenv.net;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static async Task Main(string[] args)
    {
        DotEnv.Load();

        string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        string containerName = Environment.GetEnvironmentVariable("CONTAINER_NAME");
        string filePath = @"C:\Users\atash\OneDrive\Desktop\New Microsoft Publisher Document.pub";

        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            // Add other logging providers as needed
        });
        var logger = loggerFactory.CreateLogger<BlobStorageUploader>();

        BlobStorageUploader uploader = new BlobStorageUploader(connectionString, containerName, logger);
        await uploader.UploadFileAsync(filePath);
    }
}