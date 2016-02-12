using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FabricamContactsDataAccess;

namespace Fabricam.Controllers
{
    public class ContactViewModel
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Organisation { get; set; }
        public string Title { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoinDate { get; set; }
        public byte[] Picture { get; set; }
        public int ManagerId { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public List<Contact> PossibleManagers { get; set; }
    }
}