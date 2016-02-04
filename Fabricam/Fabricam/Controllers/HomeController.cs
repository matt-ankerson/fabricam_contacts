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
        private ContactUtilities contactUtilities;

        public HomeController()
        {
            contactUtilities = new ContactUtilities(new ContactRepository(new FabricamContactsDbContext()));
        }

        public ActionResult Index()
        {
            // Get all contacts
            List<Contact> contacts = contactUtilities.GetAllContacts();

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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}