using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FabricamContactsDataAccess;

namespace FabricamContactsBusinessLogic
{
    /// <summary>
    /// Provides utilities for contacts in the database.
    /// </summary>
    public class ContactUtilities
    {
        private IContactRepository contactRepository;

        public ContactUtilities()
        {
            // Create contact repository, hand in dbContext.
            this.contactRepository = new ContactRepository(new FabricamContactsDbContext());
        }

        /// <summary>
        /// Get all contacts, ordered by last name.
        /// </summary>
        /// <returns>List of contacts</returns>
        public List<Contact> GetAllContacts()
        {
            List<Contact> allContacts;

            try
            {
                allContacts = contactRepository.GetContacts().OrderBy(x => x.LastName).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return allContacts;
        }

        /// <summary>
        /// Create new contact.
        /// </summary>
        /// <remarks>
        /// Set up relationship between worker and manager of necessary.
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
        /// <param name="managerId">The direct manager of this contact.</param>
        public bool CreateContact(string firstName, string lastName, string email, string phone, string organisation,
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
                contactRepository.InsertContact(newContact);
                contactRepository.Save();
            }
            catch (Exception)
            {
                contactCreated = false;
            }

            return contactCreated;
        }

        
    }

    
}
