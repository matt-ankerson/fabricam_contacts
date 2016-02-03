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
        public ActionResult Index()
        {
            // Get all contacts
            List<Contact> contacts = ReadContactsUtilities.GetAllContacts();
            return View(contacts);
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