using Microsoft.AspNetCore.Http;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Patterns.FactoryAssoc
{
    public abstract class AbstractAssocFactory 
    {
        public IAssoc CreateAssociationFromAssocFactory(IFormCollection data)
        {
            IAssoc association = CreateAssociation(data);
            return association;

        }
        public abstract IAssoc CreateAssociation(IFormCollection data);
    }
}

