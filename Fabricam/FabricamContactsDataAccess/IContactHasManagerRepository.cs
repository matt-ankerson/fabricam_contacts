using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricamContactsDataAccess
{
    public interface IContactHasManagerRepository : IDisposable
    {
        IEnumerable<ContactHasManager> GetContactHasManagers();
        ContactHasManager GetContactHasManagerById(int contactHasManagerId);
        void InsertContactHasManager(ContactHasManager contactHasManager);
        void DeleteContactHasManager(int contactHasManagerId);
        void UpdateContactHasManager(ContactHasManager contactHasManager);
        void Save();
    }
}
