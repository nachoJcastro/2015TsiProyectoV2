using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site
{
    public class BlobStorage
    {

        public CloudBlobContainer GetContainerTienda(string contenedor)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnection"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContianer = blobClient.GetContainerReference(contenedor);
            if (blobContianer.CreateIfNotExists())
            {
                blobContianer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            }
            return blobContianer;

        }
    }
}