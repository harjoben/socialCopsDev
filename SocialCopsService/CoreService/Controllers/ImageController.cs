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
        ImageHelper helper = new ImageHelper();
        public string SavePicture(byte[] picture, string pictureName)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container 
            CloudBlobContainer container =
                blobClient.GetContainerReference("pictures");
            container.CreateIfNotExist();
            string uniqueBlobName = string.Format("{0}{1}.jpg", pictureName,"original");
            CloudBlob blob = container.GetBlobReference(uniqueBlobName);
            // Create or overwrite the blob with content
            var ms = new MemoryStream(picture);
            blob.UploadFromStream(ms);
            string url = blob.Uri.OriginalString;
            ms.Close();
            blob=container.GetBlobReference(uniqueBlobName
            return url;
        }

        public static Image ResizeImage(Image image, Size size,bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

    }
}