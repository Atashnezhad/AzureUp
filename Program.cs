using AzureProject;

class Program
{
    static async Task Main(string[] args)
    {
        string connectionString = "your_connection_string";
        string folderPath = "path_to_your_folder";
        string containerName = "your_container_name";
        string blobName = "your_blob_name";

        BlobStorageManager storageManager = new BlobStorageManager(connectionString);
        await storageManager.UploadFolderAsZipAsync(folderPath, containerName, blobName);

        Console.WriteLine("Folder uploaded as zip to Azure Blob Storage.");
    }
}