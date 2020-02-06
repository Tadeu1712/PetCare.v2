using Microsoft.AspNetCore.Http;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Models;
using PetCareFinalVersion.Patterns.FactoryUser;

namespace PetCareFinalVersion.Patterns.FactoryAssoc
{
    public sealed class AssociationFactory : AbstractAssocFactory
    {
        private readonly AbstractUserFactory user_factory = UserFactory.Instance;


        private static readonly AssociationFactory mInstance = new AssociationFactory();

        private AssociationFactory() { }
        static AssociationFactory() { }

        public static AssociationFactory Instance => mInstance;


        public override IAssoc CreateAssociation(IAssoc aAssociation)
        {
            var user = (User)user_factory.CreateUserFromFactory(aAssociation.User);

            var association = new Association() 
            {
                Iban = aAssociation.Iban,
                Address = aAssociation.Address,
                PhoneNumber = aAssociation.PhoneNumber,
                Description = aAssociation.Description,
                FoundationDate = aAssociation.FoundationDate,
                User = user
            };

            return association;
        }
    }
}
