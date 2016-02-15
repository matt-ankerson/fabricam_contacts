using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.IO;

namespace FabricamContactsDataAccess
{
    /// <summary>
    /// For Testing. Drop, create and seed the database every time the app is started.
    /// </summary>
    public class DropCreateFabricamContactsAlways : DropCreateDatabaseAlways<FabricamContactsDbContext>
    {
        // Create the context
        private FabricamContactsDbContext dbContext = new FabricamContactsDbContext();

        protected override void Seed(FabricamContactsDbContext context)
        {
            base.Seed(context);

            // Populate tables
            PopulateContacts();
        }


        private void PopulateContacts()
        {
            List <Contact> contacts = new List<Contact>();

            contacts.Add(new Contact
            {
                FirstName = "Nigel",
                LastName = "Richards",
                DateOfBirth = new DateTime(1989, 11, 3),
                Email = "nigel@gmail.com",
                JoinDate = DateTime.Today,
                Organisation = "Contoso",
                Phone = "123456",
                Title = "Developer",
                Picture = null
            });
            contacts.Add(new Contact {
                FirstName = "Susan",
                LastName = "Holmes",
                DateOfBirth = new DateTime(1973, 5, 6),
                Email = "sue@gmail.com",
                JoinDate = new DateTime(2003, 5, 4),
                Organisation = "Pipes Inc.",
                Phone = "123456",
                Title = "Plumber",
                Picture = null,
                ManagerId = 1
            });
            contacts.Add(new Contact {
                FirstName = "Rob",
                LastName = "Brussel",
                DateOfBirth = new DateTime(1992, 10, 18),
                Email = "rob@gmail.com",
                JoinDate = new DateTime(2009, 12, 30),
                Organisation = "Royal Mail",
                Phone = "123456",
                Title = "Mail Man",
                Picture = null,
                ManagerId = 1
            });
            contacts.Add(new Contact {
                FirstName = "Faye",
                LastName = "King",
                DateOfBirth = new DateTime(1981, 8, 3),
                Email = "faye@gmail.com",
                JoinDate = new DateTime(2012, 4, 10),
                Organisation = "Contoso",
                Phone = "123456",
                Title = "Developer",
                Picture = null,
                ManagerId = 1
            });
            contacts.Add(new Contact {
                FirstName = "Lenny",
                LastName = "Newman",
                DateOfBirth = new DateTime(1958, 3, 19),
                Email = "lenny@gmail.com",
                JoinDate = new DateTime(2014, 11, 3),
                Organisation = "BBC",
                Phone = "123456",
                Title = "Broadcaster",
                Picture = null,
                ManagerId = 1
            });

            foreach (var contact in contacts)
            {
                dbContext.Contacts.Add(contact);
            }

            dbContext.SaveChanges();
        }
    }
}