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
            populateContacts();
            populateContactHasManagers();
        }

        private void populateContactHasManagers()
        {
            List<ContactHasManager> relationships = new List<ContactHasManager>();

            relationships.Add(new ContactHasManager { ManagerContactId = 1, WorkerContactId = 2});
            relationships.Add(new ContactHasManager { ManagerContactId = 1, WorkerContactId = 3 });
            relationships.Add(new ContactHasManager { ManagerContactId = 1, WorkerContactId = 4 });
            relationships.Add(new ContactHasManager { ManagerContactId = 1, WorkerContactId = 5 });

            foreach (var contactHasManager in relationships)
            {
                dbContext.ContactHasManagers.Add(contactHasManager);
            }
            dbContext.SaveChanges();
        }

        private void populateContacts()
        {
            List <Contact> contacts = new List<Contact>();

            // Make a blank image.
            Image blankImage = new Bitmap(100, 100);
            MemoryStream ms = new MemoryStream();
            blankImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] blankBytes = ms.ToArray();

            contacts.Add(new Contact
            {
                FirstName = "Nigel",
                LastName = "Richards",
                DateOfBirth = DateTime.Today,
                Email = "nigel@gmail.com",
                JoinDate = DateTime.Today,
                Organisation = "Contoso",
                Phone = "123456",
                Title = "Developer",
                Picture = blankBytes
            });
            contacts.Add(new Contact {
                FirstName = "Susan",
                LastName = "Holmes",
                DateOfBirth = DateTime.Today,
                Email = "sue@gmail.com",
                JoinDate = DateTime.Today,
                Organisation = "Contoso",
                Phone = "123456",
                Title = "Developer",
                Picture = blankBytes
            });
            contacts.Add(new Contact {
                FirstName = "Rob",
                LastName = "Brussel",
                DateOfBirth = DateTime.Today,
                Email = "rob@gmail.com",
                JoinDate = DateTime.Today,
                Organisation = "Contoso",
                Phone = "123456",
                Title = "Consultant",
                Picture = blankBytes
            });
            contacts.Add(new Contact {
                FirstName = "Faye",
                LastName = "King",
                DateOfBirth = DateTime.Today,
                Email = "faye@gmail.com",
                JoinDate = DateTime.Today,
                Organisation = "Contoso",
                Phone = "123456",
                Title = "Developer",
                Picture = blankBytes
            });
            contacts.Add(new Contact {
                FirstName = "Lenny",
                LastName = "Newman",
                DateOfBirth = DateTime.Today,
                Email = "lenny@gmail.com",
                JoinDate = DateTime.Today,
                Organisation = "Contoso",
                Phone = "123456",
                Title = "Consultant",
                Picture = blankBytes
            });

            foreach (var contact in contacts)
            {
                dbContext.Contacts.Add(contact);
            }

            dbContext.SaveChanges();
        }
    }
}