using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace FabricamContactsDataAccess
{
    public class FabricamContactsDbContext : DbContext
    {
        public FabricamContactsDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserTokenCache> UserTokenCacheList { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactHasManager> ContactHasManagers { get; set; }
    }

    
}
