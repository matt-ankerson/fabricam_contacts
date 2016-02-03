using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FabricamContactsDataAccess;

namespace FabricamContactsBusinessLogic
{
    public static class ReadContactsUtilities
    {
        /// <summary>
        /// Get all contacts in alphabetical order of last name.
        /// </summary>
        /// <returns>List of contacts</returns>
        public static List<Contact> GetAllContacts()
        {
            List<Contact> allContacts = new List<Contact>();

            try
            {
                using (var context = new FabricamContactsDbContext())
                {
                    allContacts = context.Contacts.OrderBy(x => x.LastName).ToList();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return allContacts;
        }
    }
}
