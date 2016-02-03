using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricamContactsDataAccess
{
    public interface IContactRepository : IDisposable
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContactById(int contactId);
        void InsertContact(Contact contact);
        void DeleteContact(int contactId);
        void UpdateContact(Contact contact);
        void Save();
    }
}
