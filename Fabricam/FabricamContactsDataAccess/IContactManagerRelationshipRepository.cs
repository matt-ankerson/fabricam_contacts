using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricamContactsDataAccess
{
    public interface IContactManagerRelationshipRepository : IDisposable
    {
        IEnumerable<ContactManagerRelationship> GetContactManagerRelationships();
        ContactManagerRelationship GetContactManagerRelationshipById(int contactManagerRelationshipId);
        void InsertContactManagerRelationship(ContactManagerRelationship contactManagerRelationship);
        void DeleteContactManagerRelationship(int contactManagerRelationshipId);
        void UpdateContactManagerRelationship(ContactManagerRelationship contactManagerRelationship);
        void Save();
    }
}
