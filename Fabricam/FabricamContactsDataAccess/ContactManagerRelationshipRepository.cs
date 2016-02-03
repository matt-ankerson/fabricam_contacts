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
    public class ContactManagerRelationshipRepository
    {
        private FabricamContactsDbContext context;
        private bool disposed = false;

        public ContactManagerRelationshipRepository(FabricamContactsDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ContactManagerRelationship> GetContactManagerRelationships()
        {
            return context.ContactManagerRelationships.ToList();
        }

        public ContactManagerRelationship GetContactManagerRelationshipById(int contactManagerRelationshipId)
        {
            return context.ContactManagerRelationships.Find(contactManagerRelationshipId);
        }

        public void InsertContactManagerRelationship(ContactManagerRelationship contactManagerRelationship)
        {
            context.ContactManagerRelationships.Add(contactManagerRelationship);
        }

        public void DeleteContactManagerRelationship(int contactManagerRelationshipId)
        {
            ContactManagerRelationship contactManagerRelationshipToDelete = context.ContactManagerRelationships.Find(contactManagerRelationshipId);
            context.ContactManagerRelationships.Remove(contactManagerRelationshipToDelete);
        }

        public void UpdateContactManagerRelationship(ContactManagerRelationship contactManagerRelationship)
        {
            context.Entry(contactManagerRelationship).State = EntityState.Modified;
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
