using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace CoreService.Controllers
{
    public class ImageHelper
    {
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public byte[] convertToMainImage(byte[] byteArrayIn)
        {
            Image main = byteArrayToImage(byteArrayIn);
            Size mainSize=new Size(400,400);
            Image Output=ResizeImage(main, mainSize, true);
            return imageToByteArray(Output);
        }

        public byte[] convertToThumbnail1(byte[] byteArrayIn)
        {
            Image main = byteArrayToImage(byteArrayIn);
            Size mainSize = new Size(200, 200);
            Image Output = ResizeImage(main, mainSize, true);
            return imageToByteArray(Output);
        }

        public byte[] convertToThumbnail2(byte[] byteArrayIn)
        {
            Image main = byteArrayToImage(byteArrayIn);
            Size mainSize = new Size(100, 100);
            Image Output = ResizeImage(main, mainSize, true);
            return imageToByteArray(Output);
        }

        public static Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
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