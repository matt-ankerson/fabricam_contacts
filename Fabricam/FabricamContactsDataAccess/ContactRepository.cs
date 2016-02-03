using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricamContactsDataAccess
{
    /// <summary>
    /// Repository pattern for facilitating access to the data.
    /// </summary>
    public class ContactRepository : IContactRepository
    {
        private FabricamContactsDbContext context;
        private bool disposed = false;

        public ContactRepository(FabricamContactsDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Contact> GetContacts()
        {
            return context.Contacts.ToList();
        }

        public Contact GetContactById(int contactId)
        {
            return context.Contacts.Find(contactId);
        }

        public void InsertContact(Contact contact)
        {
            context.Contacts.Add(contact);
        }

        public void DeleteContact(int contactId)
        {
            Contact contactToDelete = context.Contacts.Find(contactId);
            context.Contacts.Remove(contactToDelete);
        }

        public void UpdateContact(Contact contact)
        {
            context.Entry(contact).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
