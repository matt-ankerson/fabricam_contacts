using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using FabricamContactsDataAccess;
using FabricamContactsBusinessLogic;

namespace Fabricam.Controllers
{
    public class EditController : Controller
    {
        private ContactUtilities _contactUtilities;

        public EditController()
        {
            _contactUtilities = new ContactUtilities(new ContactRepository(new FabricamContactsDbContext()));
        }

        // GET: Edit
        public ActionResult Edit(string contactId)
        {
            ContactViewModel flatContact = GetViewModelForEdit(Convert.ToInt16(contactId));

            return View(flatContact);
        }

        public ActionResult EditContact(string contactId, string firstName, string lastName, string organisation, string title, string email,
            string phone, string managerId, HttpPostedFileBase picture, string dateOfBirth, string dateJoined)
        {
            DateTime realDateOfBirth;
            DateTime realJoinDate;
            int? realManagerId = null;
            Image realPicture = null;
            int realContactId = 0;

            try
            {
                try
                {
                    // Try to parse our date strings.
                    realDateOfBirth = DateTime.Parse(dateOfBirth);
                    realJoinDate = DateTime.Parse(dateJoined);
                }
                catch (Exception)
                {
                    throw new Exception("Invalid date/s supplied.");
                }

                // Validate other input data
                if ((string.IsNullOrEmpty(contactId)) ||
                    (string.IsNullOrEmpty(firstName)) ||
                    (string.IsNullOrEmpty(lastName)) ||
                    (string.IsNullOrEmpty(organisation)) ||
                    (string.IsNullOrEmpty(title)) ||
                    (string.IsNullOrEmpty(email)) ||
                    (string.IsNullOrEmpty(phone)))
                {
                    throw new Exception("Missing information. Please fill out all fields.");
                }

                if (!_contactUtilities.ValidateEmail(email))
                {
                    throw new Exception("Invalid email address supplied.");
                }

                if (Convert.ToInt16(contactId) != 0)
                {
                    realContactId = Convert.ToInt16(contactId);
                }

                if (Convert.ToInt16(managerId) != 0)
                {
                    realManagerId = Convert.ToInt16(managerId);
                }

                // Verify that the user selected a file, send contact to business logic layer.
                if (picture != null && picture.ContentLength > 0)
                {
                    realPicture = Image.FromStream(picture.InputStream, true, true);
                }

                if (_contactUtilities.UpdateContact(realContactId, firstName, lastName, email, phone, organisation, title,
                    realDateOfBirth, realJoinDate, realPicture, realManagerId))
                {
                    // Update of contact worked!
                }
                else
                {
                    throw new Exception("Unable to update contact. Review information supplied.");
                }

            }
            catch (Exception exception)
            {
                // Get the appropriate view model for the edit screen again.
                ContactViewModel flatContact = GetViewModelForEdit(Convert.ToInt16(contactId));

                ViewBag.Message = exception.Message;

                return View("Edit", flatContact);
            }
            // Redirect to 'show all' page.
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Fetch the indicated contact, build flat ViewModel for the Edit screen.
        /// </summary>
        /// <param name="contactId">The contact to build the view model for.</param>
        /// <returns>The view model for the edit screen.</returns>
        public ContactViewModel GetViewModelForEdit(int contactId)
        {
            // Get the indicated contact
            Contact contact = _contactUtilities.GetContactById(contactId);

            // Transfer into our flat ViewModel class
            ContactViewModel flatContact = new ContactViewModel
            {
                ContactId = contact.ContactId,
                DateOfBirth = contact.DateOfBirth,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                JoinDate = contact.JoinDate,
                Organisation = contact.Organisation,
                Phone = contact.Phone,
                Title = contact.Title
            };

            if (contact.Manager != null)
            {
                flatContact.ManagerId = contact.Manager.ContactId;
                flatContact.ManagerFirstName = contact.Manager.FirstName;
                flatContact.ManagerLastName = contact.Manager.LastName;
            }

            if (contact.Picture != null)
            {
                flatContact.Picture = contact.Picture;
            }

            // Get all possible managers for this contact (all contacts excluding themself).
            flatContact.PossibleManagers = _contactUtilities.GetAllContactsExceptOne(contactId);

            return flatContact;
        }
    }
}