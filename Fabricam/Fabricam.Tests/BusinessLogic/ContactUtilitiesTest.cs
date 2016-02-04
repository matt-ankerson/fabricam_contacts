using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FabricamContactsBusinessLogic;
using FabricamContactsDataAccess;
using Moq;
using Moq.Matchers;

namespace Fabricam.Tests.BusinessLogic
{
    [TestClass]
    public class ContactUtilitiesTest
    {
        private ContactUtilities CreateContactUtilities(bool throwsException)
        {
            Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();

            if (throwsException)
            {
                mockRepository.Setup(item => item.InsertContact(It.IsAny<Contact>()))
                    .Throws(new Exception());
            }

            return new ContactUtilities(mockRepository.Object);

        }

        [TestMethod]
        public void TestContactCreatedHasManager()
        {
            // Arrange
            ContactUtilities contactUtilities = CreateContactUtilities(true);

            // Act
            bool createResult = contactUtilities.CreateContact("Nigel", "Holmes", "nigelholmes@gmail.com", "123456", "Contoso",
                "Developer", DateTime.Today, DateTime.Today);


            // Assert
            Assert.IsFalse(createResult);
        }
    }
}
