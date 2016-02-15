using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private ContactUtilities _createContactUtilities(bool throwsException)
        {
            Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();

            if (throwsException)
            {
                mockRepository.Setup(item => item.InsertContact(It.IsAny<Contact>()))
                    .Throws(new Exception());
            }

            return new ContactUtilities(mockRepository.Object);

        }

        private Mock<DbSet<Contact>> _createContactDbSet()
        {
            // From MSDN: In order to be able to execute queries against our DbSet test double we need to setup an implementation of IQueryable.

            // Create some in-memory data
            var data = new List<Contact>
            {
                new Contact {ContactId = 1, LastName = "Kenny"},
                new Contact {ContactId = 2, LastName = "Arbour", ManagerId = 1},
                new Contact {ContactId = 3, LastName = "Morris", ManagerId = 1}
            }.AsQueryable();

            // Create a DbSet<Context>
            var mockSet = new Mock<DbSet<Contact>>();
            // Wire up the IQueryable implementation for the DbSet.
            mockSet.As<IQueryable<Contact>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Contact>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }

        [TestMethod]
        public void TestCreateContactThrowsException()
        {
            // Arrange
            ContactUtilities contactUtilities = _createContactUtilities(true);

            // Act
            bool createResult = contactUtilities.CreateContact("Nigel", "Holmes", "nigelholmes@gmail.com", "123456", "Contoso",
                "Developer", DateTime.Today, DateTime.Today);

            // Assert
            Assert.IsFalse(createResult);
        }

        [TestMethod]
        public void TestCreateContact()
        {
            // Arrange
            ContactUtilities contactUtilities = _createContactUtilities(false);

            // Act
            bool createResult = contactUtilities.CreateContact("Nigel", "Holmes", "nigelholmes@gmail.com", "123456", "Contoso",
                "Developer", DateTime.Today, DateTime.Today);

            // Assert
            Assert.IsTrue(createResult);
        }

        [TestMethod]
        public void TestUpdateContact()
        {
            // Create and set up the mock repository
            Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();
            mockRepository.Setup(x => x.UpdateContact(It.IsAny<Contact>()));
            // Updating a contact involves calling up the existing record - we need to mock this behaviour.
            mockRepository.Setup(x => x.GetContactById(It.Is<int>(y => y == 1))).Returns(new Contact
            {
                FirstName = "Nigel",
                LastName = "Richards",
                DateOfBirth = DateTime.Today,
                Email = "nigel@gmail.com",
                JoinDate = DateTime.Today,
                Organisation = "Contoso",
                Phone = "123456",
                Title = "Developer",
                Picture = null
            });

            ContactUtilities contactUtilities = new ContactUtilities(mockRepository.Object);

            // Run the code we want to test, keep the result.
            bool updateResult = contactUtilities.UpdateContact(1, "Austin", "Morris", "austin@gmail.com", "5438756",
                "BBC", "Broadcaster", DateTime.Today, DateTime.Today, null, null);

            Assert.IsTrue(updateResult);
        }

        [TestMethod]
        public void TestUpdateContactThrowsException()
        {
            // Create and set up the mock repository
            Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();
            // UpdateContact should throw an exception.
            mockRepository.Setup(x => x.UpdateContact(It.IsAny<Contact>())).Throws(new Exception());
            // Updating a contact involves calling up the existing record - we need to mock this behaviour.
            mockRepository.Setup(x => x.GetContactById(It.Is<int>(y => y == 1))).Returns(new Contact
            {
                FirstName = "Nigel",
                LastName = "Richards",
                DateOfBirth = DateTime.Today,
                Email = "nigel@gmail.com",
                JoinDate = DateTime.Today,
                Organisation = "Contoso",
                Phone = "123456",
                Title = "Developer",
                Picture = null
            });

            ContactUtilities contactUtilities = new ContactUtilities(mockRepository.Object);

            // Run the code we want to test, keep the result.
            bool updateResult = contactUtilities.UpdateContact(1, "Austin", "Morris", "austin@gmail.com", "5438756",
                "BBC", "Broadcaster", DateTime.Today, DateTime.Today, null, null);

            Assert.IsFalse(updateResult);
        }

        [TestMethod]
        public void TestGetContactById()
        {
            Contact expected = new Contact
            {
                FirstName = "Nigel",
                LastName = "Richards",
                DateOfBirth = DateTime.Today,
                Email = "nigel@gmail.com",
                JoinDate = DateTime.Today,
                Organisation = "Contoso",
                Phone = "123456",
                Title = "Developer",
                Picture = null
            };

            // Set up the mock
            Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();

            // Indicate what the mock should accept, and what it should return.
            mockRepository.Setup(x => x.GetContactById(It.Is<int>(y => y == 1))).Returns(new Contact
            {
                FirstName = "Nigel",
                LastName = "Richards",
                DateOfBirth = DateTime.Today,
                Email = "nigel@gmail.com",
                JoinDate = DateTime.Today,
                Organisation = "Contoso",
                Phone = "123456",
                Title = "Developer",
                Picture = null
            });

            // Instantiate the biz logic class we plan to test a component of, pass in the mocked repository
            ContactUtilities contactUtilities = new ContactUtilities(mockRepository.Object);

            // Run the code we want to test, keep the result.
            Contact actual = contactUtilities.GetContactById(1);

            // Compare with what we expected to get.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetContacts()
        {
            // GetAllContacts() returns all contacts, ordered by lastname.

            Mock<DbSet<Contact>> mockSet = _createContactDbSet();

            // Set up the mock
            Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();
            mockRepository.Setup(x => x.GetContacts()).Returns(mockSet.Object);

            // Instantiate the biz logic class we plan to test a component of, pass in the mocked repository
            ContactUtilities contactUtilities = new ContactUtilities(mockRepository.Object);

            // Run the code we want to test, keep the result.
            List<Contact> actualContacts = contactUtilities.GetAllContacts();

            Assert.AreEqual(3, actualContacts.Count);
            Assert.AreEqual("Arbour", actualContacts[0].LastName);
            Assert.AreEqual("Kenny", actualContacts[1].LastName);
            Assert.AreEqual("Morris", actualContacts[2].LastName);
        }

        [TestMethod]
        public void TestGetAllContactsExceptOne()
        {
            Mock<DbSet<Contact>> mockSet = _createContactDbSet();

            // Set up the mock
            Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();
            mockRepository.Setup(x => x.GetContacts()).Returns(mockSet.Object);

            // Instantiate the biz logic class we plan to test a component of, pass in the mocked repository
            ContactUtilities contactUtilities = new ContactUtilities(mockRepository.Object);

            // Run the code we want to test, keep the result.
            List<Contact> actualContacts = contactUtilities.GetAllContactsExceptOne(1);

            Assert.AreEqual(2, actualContacts.Count);
        }

        [TestMethod]
        public void TestDeleteContactWhoIsNotAManager()
        {
            Mock<DbSet<Contact>> mockSet = _createContactDbSet();

            // Set up the mock repository
            Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();
            mockRepository.Setup(x => x.DeleteContact(It.Is<int>(y => y == 3)));
            mockRepository.Setup(x => x.GetContacts()).Returns(mockSet.Object);

            // Instantiate the biz logic class we plan to test a component of, pass in the mocked repository
            ContactUtilities contactUtilities = new ContactUtilities(mockRepository.Object);

            // Run the code we want to test, keep the results
            bool contactDeleted = contactUtilities.DeleteContact(3);

            Assert.IsTrue(contactDeleted);
        }

        [TestMethod]
        public void TestDeleteContactWhoIsAManager()
        {
            Mock<DbSet<Contact>> mockSet = _createContactDbSet();

            // Set up the mock repository
            Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();
            mockRepository.Setup(x => x.DeleteContact(It.Is<int>(y => y == 1)));
            mockRepository.Setup(x => x.GetContacts()).Returns(mockSet.Object);

            // Instantiate the biz logic class we plan to test a component of, pass in the mocked repository
            ContactUtilities contactUtilities = new ContactUtilities(mockRepository.Object);

            // Run the code we want to test, keep the results
            bool contactDeleted = contactUtilities.DeleteContact(1);

            Assert.IsFalse(contactDeleted);
        }

        [TestMethod]
        public void TestDeleteContactThrowsException()
        {
            Mock<DbSet<Contact>> mockSet = _createContactDbSet();

            // Set up the mock repository
            Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();
            mockRepository.Setup(x => x.DeleteContact(It.IsAny<int>())).Throws(new Exception()); // DeleteContact will raise an exception.
            mockRepository.Setup(x => x.GetContacts()).Returns(mockSet.Object);

            // Instantiate the biz logic class we plan to test a component of, pass in the mocked repository
            ContactUtilities contactUtilities = new ContactUtilities(mockRepository.Object);

            // Run the code we want to test, keep the results
            bool contactDeleted = contactUtilities.DeleteContact(1);

            Assert.IsFalse(contactDeleted);
        }

        [TestMethod]
        public void TestValidateEmail()
        {
            Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();
            ContactUtilities contactUtilities = new ContactUtilities(mockRepository.Object);

            bool result = contactUtilities.ValidateEmail("mr.matt@gmail.com");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestValidateEmailFails()
        {
            Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();
            ContactUtilities contactUtilities = new ContactUtilities(mockRepository.Object);

            bool result = contactUtilities.ValidateEmail("mr.matt&gmail.com");

            Assert.IsFalse(result);
        }
    }
}
