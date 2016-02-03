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
    public class ContactHasManagerRepository
    {
        private FabricamContactsDbContext context;
        private bool disposed = false;

        public ContactHasManagerRepository(FabricamContactsDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ContactHasManager> GetContactHasManagers()
        {
            return context.ContactHasManagers.ToList();
        }

        public ContactHasManager GetContactHasManagerById(int contactHasManagerId)
        {
            return context.ContactHasManagers.Find(contactHasManagerId);
        }

        public void InsertContactHasManager(ContactHasManager contactHasManager)
        {
            context.ContactHasManagers.Add(contactHasManager);
        }

        public void DeleteContactHasManager(int contactHasManagerId)
        {
            ContactHasManager contactHasManagerToDelete = context.ContactHasManagers.Find(contactHasManagerId);
            context.ContactHasManagers.Remove(contactHasManagerToDelete);
        }

        public void UpdateContactHasManager(ContactHasManager contactHasManager)
        {
            context.Entry(contactHasManager).State = EntityState.Modified;
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
