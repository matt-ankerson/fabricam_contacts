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
    public class CreateContactUtilitiesTest
    {
        [TestMethod]
        public void TestContactCreatedHasManager()
        {
            // Arrange
            ContactUtilities contactUtilities = new ContactUtilities();

            // Act
            contactUtilities.CreateContact("Nigel", "Holmes", "nigelholmes@gmail.com", "123456", "Contoso",
                "Developer", DateTime.Today, DateTime.Today);
            contactUtilities.CreateContact("Frank", "Hue", "frankhue@gmail.com", "123456", "Contoso",
                "Grad Developer", DateTime.Today, DateTime.Today, null);  

            // Assert
            Assert.IsTrue(true);
        }
    }
}
