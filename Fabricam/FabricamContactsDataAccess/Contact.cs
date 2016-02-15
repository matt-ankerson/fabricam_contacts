using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricamContactsDataAccess
{
    /// <summary>
    /// Holds information necessary for a single contact
    /// </summary>
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Organisation { get; set; }
        public string Title { get; set; }
        public byte[] Picture { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoinDate { get; set; }

        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }

        public virtual Contact Manager { get; set; }
        public virtual ICollection<Contact> Workers { get; set; }

        // Comparator overrided to provide comparisons in unit testing.
        public override bool Equals(object obj)
        {
            Contact other = obj as Contact;

            if (other == null)
            {
                return false;
            }

            return ((this.ContactId == other.ContactId) && 
                (this.FirstName == other.FirstName) && 
                (this.LastName == other.LastName) &&
                (this.Email == other.Email) &&
                (this.Phone == other.Phone) &&
                (this.Organisation == other.Organisation) &&
                (this.Title == other.Title) &&
                (this.Picture == other.Picture) &&
                (this.DateOfBirth == other.DateOfBirth) &&
                (this.JoinDate == other.JoinDate) &&
                (this.ManagerId == other.ManagerId));
        }

        public override int GetHashCode()
        {
            return 33 * + ContactId.GetHashCode();
        }
    }
}