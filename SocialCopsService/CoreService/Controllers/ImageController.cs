using CoreService.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace CoreService.Controllers
{
    public class ImageController
    {
        private static ImageHelper helper = new ImageHelper();
        public static string[] SavePicture(byte[] Image,string id)
        {

            string[] asyncResult = new string[2];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container 
            CloudBlobContainer container =
                blobClient.GetContainerReference("pictures");
            container.CreateIfNotExist();
            string uniqueBlobName = string.Format("{0}{1}.jpg",id,"original");
            CloudBlob blob = container.GetBlobReference(uniqueBlobName);
            // Create or overwrite the blob with content
            var ms = new MemoryStream(Image);
            blob.UploadFromStream(ms);
            asyncResult[0] = blob.Uri.OriginalString;
            asyncResult[1] = id;
            ms.Close();
            return asyncResult;
        }

    

    }
}