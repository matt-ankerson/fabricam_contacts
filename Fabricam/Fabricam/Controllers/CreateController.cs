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
            int? realManagerID = null;

            try
            {
                try
                {
                    // Parse our date strings.
                    realDateOfBirth = DateTime.Parse(dateOfBirth);
                    realJoinDate = DateTime.Parse(dateJoined);
                }
                catch (Exception)
                {
                    throw new Exception("Invalid date/s supplied.");
                }

                if (Convert.ToInt16(managerId) != 0)
                {
                    realManagerID = Convert.ToInt16(managerId);
                }

                // Verify that the user selected a file, send contact to business logic layer.
                if (picture != null && picture.ContentLength > 0)
                {
                    Image realPicture = Image.FromStream(picture.InputStream, true, true);

                    _contactUtilities.CreateContact(firstName, lastName, email, phone, organisation, title, realDateOfBirth,
                        realJoinDate, realPicture, realManagerID);
                }
                else
                {
                    _contactUtilities.CreateContact(firstName, lastName, email, phone, organisation, title, realDateOfBirth,
                        realJoinDate, null, realManagerID);
                }
            }
            catch (Exception exception)
            {
                ViewBag.Message = exception.Message;
            }

            // Redirect to 'ahow all' page.
            return RedirectToAction("Index", "Home");
        }
    }
}