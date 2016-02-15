using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FabricamContactsBusinessLogic;
using FabricamContactsDataAccess;

namespace Fabricam.Tests.BusinessLogic
{
    [TestClass]
    public class ImageBytesConverterTest
    {
        [TestMethod]
        public void TestImageConvertsToBytes()
        {
            Image blankImage = new Bitmap(100, 100);
            byte[] blankBytes = ImageBytesConverter.ConvertImageToBytes(blankImage);
            Assert.IsNotNull(blankBytes);
        }

        [TestMethod]
        public void TestBytesConvertToImage()
        {
            var ms = new MemoryStream();
            Image rawImage = new Bitmap(100, 100);
            rawImage.Save(ms, ImageFormat.Png);

            Image blankImage = ImageBytesConverter.ConvertBytesToImage(ms.ToArray());
            Assert.IsNotNull(blankImage);
        }
    }
}
