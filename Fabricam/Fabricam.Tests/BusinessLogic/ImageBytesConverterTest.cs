using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
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
    }
}
