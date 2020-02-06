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


        public override IAssoc CreateAssociation(IFormCollection data)
        {
            var user = (User)user_factory.CreateUserFromFactory(data);

            var association = new Association() 
            {
                Iban = data["iban"][0],
                Address = data["adress"][0],
                PhoneNumber = data["phoneNumber"][0],
                Description = data["description"][0],
                FoundationDate = data["foundationDate"][0],
                User = user
            };

            return association;
        }
    }
}
