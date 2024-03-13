using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.Compute.Models;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management;
using Microsoft.Rest.Azure.Authentication;
using System;


namespace AzureProject
{
    public class AzureVirtualMachineManager
    {
        private readonly string _tenantId;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _subscriptionId;

        public AzureVirtualMachineManager(string tenantId, string clientId, string clientSecret, string subscriptionId)
        {
            _tenantId = tenantId;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _subscriptionId = subscriptionId;
        }

        public void CreateAndSetupVirtualMachines(string resourceGroupName, string[] vmNames, string adminUser, string adminPassword, string scriptUri)
        {
            var credentials = SdkContext.AzureCredentialsFactory
                .FromServicePrincipal(_clientId, _clientSecret, _tenantId, AzureEnvironment.AzureGlobalCloud);

            var azure = Azure.Configure()
                .Authenticate(credentials)
                .WithSubscription(_subscriptionId);

            azure.ResourceGroups
                .Define(resourceGroupName)
                .WithRegion(Region.USWest)
                .Create();

            var vmSize = VirtualMachineSizeTypes.StandardDS1V2;

            foreach (var vmName in vmNames)
            {
                var vm = azure.VirtualMachines
                    .Define(vmName)
                    .WithRegion(Region.USWest)
                    .WithExistingResourceGroup(resourceGroupName)
                    .WithNewPrimaryNetwork("10.0.0.0/24")
                    .WithPrimaryPrivateIPAddressDynamic()
                    .WithNewPrimaryPublicIPAddress(vmName + "publicip")
                    .WithPopularLinuxImage(KnownLinuxVirtualMachineImage.UbuntuServer16_04_LTS)
                    .WithRootUsername(adminUser)
                    .WithRootPassword(adminPassword)
                    .WithSize(vmSize)
                    .Create();

                AttachCustomScriptExtension(vm, scriptUri, vmName);
            }

            Console.WriteLine("Virtual machines created successfully.");
        }

        private void AttachCustomScriptExtension(IVirtualMachine vm, string scriptUri, string vmName)
        {
            vm.Update()
                .DefineNewExtension("CustomScript")
                .WithPublisher("Microsoft.OSTCExtensions")
                .WithType("CustomScriptForLinux")
                .WithVersion("1.5")
                .WithMinorVersionAutoUpgrade()
                .WithPublicSetting("fileUris", new[] { scriptUri })
                .WithPublicSetting("commandToExecute", $"bash install_script.sh {vmName}")
                .Attach();
        }
    }
}
