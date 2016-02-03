using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace FabricamContactsBusinessLogic
{
    /// <summary>
    /// Utilities for converting images to bytes and back again.
    /// </summary>
    public static class ImageBytesConverter
    {
        public static byte[] ConvertImageToBytes(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public static Image ConvertBytesToImage(byte[] byteArray)
        {
            MemoryStream ms = new MemoryStream(byteArray);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
