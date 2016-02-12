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
        private readonly IContactRepository _contactRepository;

        public ContactUtilities(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
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
                allContacts = _contactRepository.GetContacts().OrderBy(x => x.LastName).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return allContacts;
        }

        public Contact GetContactById(int contactId)
        {
            try
            {
                return _contactRepository.GetContactById(contactId);
            }
            catch (Exception exception)
            {   
                throw exception;
            }
        }

        /// <summary>
        /// Get all contacts except one. Useful for getting all potential managers (A contact can't manage themselves).
        /// </summary>
        /// <param name="contactId">The contact to exclude from the list.</param>
        /// <returns>List of contacts, excluding the indicated contact.</returns>
        public List<Contact> GetAllContactsExceptOne(int contactId)
        {
            try
            {
                return _contactRepository.GetContacts().Where(x => x.ContactId != contactId).ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
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

                if (picture != null)
                {
                    // Use provided image
                    byte[] imageBytes = ImageBytesConverter.ConvertImageToBytes(picture);
                    newContact.Picture = imageBytes;
                }
                

                // Save
                _contactRepository.InsertContact(newContact);
                _contactRepository.Save();
            }
            catch (Exception exception)
            {
                contactCreated = false;
            }

            return contactCreated;
        }

        public bool UpdateContact(int contactId, string firstName, string lastName, string email, string phone, string organisation,
            string title, DateTime dateOfBirth, DateTime dateJoined, Image picture = null, int? managerId = null)
        {
            bool contactUpdated = true;

            try
            {
                // Pull the old contact up, update fields.
                Contact oldContact = _contactRepository.GetContactById(contactId);
                oldContact.FirstName = firstName;
                oldContact.LastName = lastName;
                oldContact.Email = email;
                oldContact.Phone = phone;
                oldContact.Organisation = organisation;
                oldContact.Title = title;
                oldContact.DateOfBirth = dateOfBirth;
                oldContact.JoinDate = dateJoined;
                oldContact.ManagerId = managerId;

                if (picture != null)
                {
                    // Use provided image
                    byte[] imageBytes = ImageBytesConverter.ConvertImageToBytes(picture);
                    oldContact.Picture = imageBytes;
                }

                _contactRepository.UpdateContact(oldContact);
                _contactRepository.Save();
            }
            catch (Exception exception)
            {
                contactUpdated = false;
            }

            return contactUpdated;
        }

        /// <summary>
        /// Delete an indicated contact.
        /// </summary>
        /// <param name="contactId">Indicates contact to delete.</param>
        /// <returns>Boolean success indicator.</returns>
        public bool DeleteContact(int contactId)
        {
            bool contactDeleted = true;

            try
            {
                // Check that this contact doesn't 'manage' any other contacts.
                if (_contactRepository.GetContacts().Select(x => x.ManagerId).ToList().Contains(contactId))
                {
                    // We can't delete this contact while it manages others.
                    contactDeleted = false;
                }
                else
                {
                    _contactRepository.DeleteContact(contactId);
                    _contactRepository.Save();
                }
                
            }
            catch (Exception)
            {
                contactDeleted = false;
            }

            return contactDeleted;
        }

        /// <summary>
        /// Ensure the supplied email address 'looks like' an email address.
        /// </summary>
        /// <remarks>
        /// Emails are not used as keys in the database, so it is not necessary to enforce uniqueness.
        /// </remarks>
        /// <param name="email">Email to check.</param>
        /// <returns>Valid or invalid email (boolean indicator)</returns>
        public bool ValidateEmail(string email)
        {
            if (email.Contains("@"))
            {
                return true;
            }

            return false;
        }


    }

    
}
