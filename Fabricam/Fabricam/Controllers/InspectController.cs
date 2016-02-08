using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FabricamContactsDataAccess;
using FabricamContactsBusinessLogic;

namespace Fabricam.Controllers
{
    public class InspectController : Controller
    {
        private ContactUtilities _contactUtilities;

        public InspectController()
        {
            _contactUtilities = new ContactUtilities(new ContactRepository(new FabricamContactsDbContext()));
        }

        // GET: Inspect
        public ActionResult Inspect(string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                return RedirectToAction("Index", "Home");
            }
            
            Contact contact = _contactUtilities.GetContactById(Convert.ToInt16(contactId));

            // Build flat contact
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
                flatContact.ManagerFirstName = contact.Manager.FirstName;
                flatContact.ManagerLastName = contact.Manager.LastName;
            }

            if (contact.Picture != null)
            {
                flatContact.Picture = contact.Picture;
            }

            // Send image to view

            return View(flatContact);
        }
    }
}