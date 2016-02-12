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

            // Make a blank image.
            //Image blankImage = new Bitmap(100, 100);
            //MemoryStream ms = new MemoryStream();
            //blankImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //byte[] blankBytes = ms.ToArray();

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
                Picture = null
            });
            contacts.Add(new Contact {
                FirstName = "Susan",
                LastName = "Holmes",
                DateOfBirth = DateTime.Today,
                Email = "sue@gmail.com",
                JoinDate = DateTime.Today,
                Organisation = "Pipes Inc.",
                Phone = "123456",
                Title = "Plumber",
                Picture = null,
                ManagerId = 1
            });
            contacts.Add(new Contact {
                FirstName = "Rob",
                LastName = "Brussel",
                DateOfBirth = DateTime.Today,
                Email = "rob@gmail.com",
                JoinDate = DateTime.Today,
                Organisation = "Royal Mail",
                Phone = "123456",
                Title = "Mail Man",
                Picture = null,
                ManagerId = 1
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
                Picture = null,
                ManagerId = 1
            });
            contacts.Add(new Contact {
                FirstName = "Lenny",
                LastName = "Newman",
                DateOfBirth = DateTime.Today,
                Email = "lenny@gmail.com",
                JoinDate = DateTime.Today,
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