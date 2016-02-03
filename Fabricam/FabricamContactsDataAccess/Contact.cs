﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int? ManagerId { get; set; }
        public byte[] Picture { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoinDate { get; set; }

        public virtual List<ContactManagerRelationship> DirectWorkers { get; set; }
    }
}