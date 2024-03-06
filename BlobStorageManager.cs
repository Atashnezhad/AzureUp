using System.IO.Compression;
using Azure.Storage.Blobs;

namespace AzureProject
{
    public class BlobStorageManager
    {
        private readonly string _connectionString;

        public BlobStorageManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task UploadFolderAsZipAsync(string folderPath, string containerName, string blobName)
        {
            // Create a temporary zip file
            string zipFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.zip");
            ZipFile.CreateFromDirectory(folderPath, zipFilePath);

            try
            {
                // Upload the zip file to Azure Blob Storage
                BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                await blobClient.UploadAsync(zipFilePath);
            }
            finally
            {
                // Delete the temporary zip file
                File.Delete(zipFilePath);
            }
        }
    }
    
}

