using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FabricamContactsDataAccess;

namespace FabricamContactsBusinessLogic
{
    /// <summary>
    /// Provides utilities for contact-manager relationships in the database
    /// </summary>
    public class ContactManagerRelationshipUtilities
    {
        private IContactManagerRelationshipRepository contactManagerRelationshipRepository;

        public ContactManagerRelationshipUtilities()
        {
            this.contactManagerRelationshipRepository = new ContactManagerRelationshipRepository(new FabricamContactsDbContext());
        }

        /// <summary>
        /// Insert a new manager/worker relationship
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="workerId"></param>
        public void CreateContactManagerRelationship(int managerId, int workerId)
        {
            ContactManagerRelationship newContactManagerRelationship = new ContactManagerRelationship
            {
                ManagerContactId = managerId,
                WorkerContactId = workerId
            };

            contactManagerRelationshipRepository.InsertContactManagerRelationship(newContactManagerRelationship);
            contactManagerRelationshipRepository.Save();
        }
    }
}
