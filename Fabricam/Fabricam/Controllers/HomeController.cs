using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FabricamContactsBusinessLogic;
using FabricamContactsDataAccess;

namespace Fabricam.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ContactUtilities _contactUtilities;

        public HomeController()
        {
            _contactUtilities = new ContactUtilities(new ContactRepository(new FabricamContactsDbContext()));
        }

        public ActionResult Index()
        {
            // Get all contacts
            List<Contact> contacts = _contactUtilities.GetAllContacts();

            //Build flat view model.
            List<ContactViewModel> flatContacts = new List<ContactViewModel>();

            foreach (var contact in contacts)
            {
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

                if (contact.Manager == null)
                {

                }
                else
                {
                    flatContact.ManagerFirstName = contact.Manager.FirstName;
                    flatContact.ManagerLastName = contact.Manager.LastName;
                }

                flatContacts.Add(flatContact);
            }

            return View(flatContacts);
        }

        [HttpGet]
        public JsonResult DeleteContact(string contactId)
        {
            if (_contactUtilities.DeleteContact(Convert.ToInt16(contactId)))
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }

            return Json("Failure", JsonRequestBehavior.DenyGet);
        }
    }
}