using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricamContactsDataAccess
{
    /// <summary>
    /// Facilitates the zero-to-many relationship between contacts. 
    /// (ie. Some contacts are managers of other contacts)
    /// </summary>
    public class ContactHasManager
    {
        public int ContactHasManagerId { get; set; }
        public int ManagerContactId { get; set; }
        public int WorkerContactId { get; set; }

        public virtual Contact ManagerContact { get; set; }
        public virtual Contact WorkerContact { get; set; }
    }
}
