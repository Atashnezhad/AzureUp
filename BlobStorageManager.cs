using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using dotenv.net;
using Microsoft.Extensions.Logging;

public class BlobStorageUploader
{
    private readonly string _connectionString;
    private readonly string _containerName;
    private readonly ILogger<BlobStorageUploader> _logger;

    public BlobStorageUploader(string connectionString, string containerName, ILogger<BlobStorageUploader> logger)
    {
        _connectionString = connectionString;
        _containerName = containerName;
        _logger = logger;
    }

    public async Task UploadFileAsync(string filePath)
    {
        try
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            // Create the container client
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(Path.GetFileName(filePath));

            // Open the file and upload it to blob storage
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                await blobClient.UploadAsync(fileStream, true);
            }

            _logger.LogInformation("File uploaded successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while uploading file to blob storage.");
            throw;
        }
    }
    
    public async Task UploadDirectoryAsync(string directoryPath)
    {
        try
        {
            string zipFileName = $"{Path.GetFileName(directoryPath)}.zip";
            string zipFilePath = Path.Combine(Path.GetDirectoryName(directoryPath), zipFileName);

            // Create a zip archive of the directory
            ZipFile.CreateFromDirectory(directoryPath, zipFilePath);

            // Upload the zip file to Azure Blob Storage
            await UploadFileAsync(zipFilePath);

            // Delete the temporary zip file
            File.Delete(zipFilePath);

            _logger.LogInformation("Directory uploaded successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while uploading directory to blob storage.");
            throw;
        }
    }
    
    
    
}

