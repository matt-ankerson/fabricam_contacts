using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FabricamContactsDataAccess;

namespace FabricamContactsBusinessLogic
{
    /// <summary>
    /// Provides utilities for creating new contacts.
    /// </summary>
    public static class CreateContactUtilities
    {
        /// <summary>
        /// Create new contact.
        /// </summary>
        /// <remarks>
        /// Set p relationship between worker and manager of necessary.
        /// </remarks>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="organisation"></param>
        /// <param name="title"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="dateJoined"></param>
        /// <param name="picture">Picture to store in database.</param>
        /// <param name="managerId">If included, this indicates the direct manager for the new contact.</param>
        public static bool CreateContact(string firstName, string lastName, string email, string phone, string organisation,
            string title, DateTime dateOfBirth, DateTime dateJoined, Image picture = null, int? managerId = null)
        {
            bool contactCreated = true;

            try
            {
                Contact newContact = new Contact
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone,
                    Organisation = organisation,
                    Title = title,
                    DateOfBirth = dateOfBirth,
                    JoinDate = dateJoined,
                    ManagerId = managerId
                };

                if (picture == null)
                {
                    // Use anonymous image
                    Image anonymousImage = Image.FromFile("Images/anon-user.png");
                    byte[] anonymousBytes = ImageBytesConverter.ConvertImageToBytes(anonymousImage);
                    newContact.Picture = anonymousBytes;
                }
                else
                {
                    // Use provided image
                    byte[] imageBytes = ImageBytesConverter.ConvertImageToBytes(picture);
                    newContact.Picture = imageBytes;
                }

                // Save
                using (var context = new FabricamContactsDbContext())
                {
                    context.Contacts.Add(newContact);
                    context.SaveChanges();
                }

                // Was a manager supplied?
                if (managerId != null)
                {
                    createContactHasManager(managerId.Value, newContact.ContactId);
                }
            }
            catch (Exception)
            {
                contactCreated = false;
            }

            return contactCreated;
        }

        /// <summary>
        /// Insert a new manager/worker relationship
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="workerId"></param>
        private static void createContactHasManager(int managerId, int workerId)
        {
            using (var context = new FabricamContactsDbContext())
            {
                ContactHasManager contactHasManager = new ContactHasManager
                {
                    ManagerContactId = managerId,
                    WorkerContactId = workerId
                };

                context.ContactHasManagers.Add(contactHasManager);
                context.SaveChanges();
            }
        }
    }
}
