# Azure VM Deployment and File Upload Automation

## Overview

This script automates the process of deploying virtual machines (VMs) in Microsoft Azure, cloning GitHub repositories
onto the VMs, retrieving specific files, and uploading them to Azure Blob Storage.

## Prerequisites

Before running the script, ensure you have the following:

- Microsoft Azure account
- Azure CLI installed and authenticated
- Git client installed
- Azure SDK for .NET installed
- .NET Core SDK installed

## Steps

### 1. List Folders on Local Machine

Use the `Directory.GetDirectories` method to retrieve a list of folders on your local machine.

```csharp
string[] folders = Directory.GetDirectories("Path to folders");
```

### 2. Instantiate a Virtual Machine (VM) in Azure

Use the Azure SDK for .NET to create a virtual machine instance in Azure. You'll need to provide details such as the VM
size, operating system image, resource group, and other configuration options.

### Example Code:

```csharp
var vm = azure.VirtualMachines.Define("VMName")
    .WithRegion(Region.USWest)
    .WithNewResourceGroup("ResourceGroupName")
    .WithNewPrimaryNetwork("10.0.0.0/24")
    .WithPrimaryPrivateIPAddressDynamic()
    .WithNewPrimaryPublicIPAddress("publicipdns")
    .WithPopularLinuxImage(KnownLinuxVirtualMachineImage.UbuntuServer18_04_Lts)
    .WithRootUsername("username")
    .WithSsh("sshKey")
    .Create();

```

### 3. Configure the VM

Use the Azure SDK to configure the VM after instantiation. This may involve setting up network configurations,
installing software packages, and configuring authentication credentials.

### 4. Clone GitHub Repository

Use a Git client library (such as LibGit2Sharp) to clone your GitHub repository onto the VM. Provide the necessary
credentials for authentication.

### 5. Retrieve Specific File

Use file I/O operations to access and retrieve the specific file you need from the cloned repository.

### 6. Upload File to Azure Blob Storage

Use the Azure SDK for .NET to connect to your Azure Blob Storage account and upload the file retrieved from the GitHub
repository.

### 7. Terminate the VM

Use the Azure SDK to deallocate and delete the VM instance once the file has been uploaded to Azure Blob Storage.

### 8. Repeat for Each Folder

Repeat steps 2 to 7 for each folder listed on your local machine.

List Folders on Local Machine:

Use the Directory.GetDirectories method to retrieve a list of folders on your local machine.
Instantiate a Virtual Machine (VM) in Azure:

Use the Azure SDK for .NET to create a virtual machine instance in Azure. You'll need to provide details such as the VM
size, operating system image, resource group, and other configuration options.
Configure the VM:

Use the Azure SDK to configure the VM after instantiation. This may involve setting up network configurations,
installing software packages, and configuring authentication credentials.
Clone GitHub Repository:

Use a Git client library (such as LibGit2Sharp) to clone your GitHub repository onto the VM. Provide the necessary
credentials for authentication.
Retrieve Specific File:

Use file I/O operations to access and retrieve the specific file you need from the cloned repository.
Upload File to Azure Blob Storage:

Use the Azure SDK for .NET to connect to your Azure Blob Storage account and upload the file retrieved from the GitHub
repository.
Terminate the VM:

Use the Azure SDK to deallocate and delete the VM instance once the file has been uploaded to Azure Blob Storage.
Repeat for Each Folder:

Repeat steps 2 to 7 for each folder listed on your local machine.