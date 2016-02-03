using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricamContactsDataAccess
{
    public interface IContactHasManagerRepository : IDisposable
    {
        IEnumerable<ContactManagerRelationship> GetContactHasManagers();
        ContactManagerRelationship GetContactHasManagerById(int contactHasManagerId);
        void InsertContactHasManager(ContactManagerRelationship contactManagerRelationship);
        void DeleteContactHasManager(int contactHasManagerId);
        void UpdateContactHasManager(ContactManagerRelationship contactManagerRelationship);
        void Save();
    }
}
