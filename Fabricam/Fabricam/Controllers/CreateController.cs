using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FabricamContactsBusinessLogic;
using FabricamContactsDataAccess;

namespace Fabricam.Controllers
{
    public class CreateController : Controller
    {
        private ContactUtilities _contactUtilities;

        public CreateController()
        {
            _contactUtilities = new ContactUtilities(new ContactRepository(new FabricamContactsDbContext()));
        }

        // GET: Create
        public ActionResult Create()
        {
            // Get all contacts, for manager selection.
            List<Contact> possibleManagers = _contactUtilities.GetAllContacts();

            return View(possibleManagers);
        }

        [HttpPost]
        public ActionResult CreateContact(string firstName, string lastName, string organisation, string title, string email,
            string phone, string managerId, HttpPostedFileBase picture, string dateOfBirth, string dateJoined)
        {
            DateTime realDateOfBirth;
            DateTime realJoinDate;
            int? realManagerId = null;
            Image realPicture = null;

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
                if ((string.IsNullOrEmpty(firstName)) ||
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

                if (Convert.ToInt16(managerId) != 0)
                {
                    realManagerId = Convert.ToInt16(managerId);
                }

                // Verify that the user selected a file, send contact to business logic layer.
                if (picture != null && picture.ContentLength > 0)
                {
                    realPicture = Image.FromStream(picture.InputStream, true, true);
                }

                if (_contactUtilities.CreateContact(firstName, lastName, email, phone, organisation, title,
                    realDateOfBirth,
                    realJoinDate, realPicture, realManagerId))
                {
                    // Insertion of new contact worked!
                }
                else
                {
                    throw new Exception("Unable to insert new contact. Review information supplied.");
                }

            }
            catch (Exception exception)
            {
                // Get all contacts, for manager selection.
                List<Contact> possibleManagers = _contactUtilities.GetAllContacts();

                ViewBag.Message = exception.Message;

                return View("Create", possibleManagers);
            }

            // Redirect to 'show all' page.
            return RedirectToAction("Index", "Home");
        }
    }
}