using System;
using System.Collections.Generic;
using System.Linq;
using BCrypt;
using System.Threading.Tasks;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Models;

namespace PetCareFinalVersion.Patterns.FactoryAssoc
{
    public sealed class AssociationFactory : AbstractAssocFactory
    {
        private static readonly AssociationFactory mInstance = new AssociationFactory();

        private AssociationFactory() { }
        static AssociationFactory() { }

        public static AssociationFactory Instance => mInstance;


        public override IUser CreateAssociation(IUser aAssociation)
        {
            var user = new User() 
            {
                Name = aAssociation.User.Name,
                Email = aAssociation.User.Email,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(aAssociation.User.Password),
                Admin = false,
            };


            var association = new Association() 
            {
                Iban = aAssociation.Iban,
                Adress = aAssociation.Adress,
                PhoneNumber = aAssociation.PhoneNumber,
                Description = aAssociation.Description,
                FoundationDate = aAssociation.FoundationDate,
                User = user,
            };

            return association;
        }
    }
}
