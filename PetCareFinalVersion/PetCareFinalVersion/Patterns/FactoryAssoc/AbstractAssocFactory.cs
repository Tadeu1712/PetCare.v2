using Microsoft.AspNetCore.Http;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Models;

namespace PetCareFinalVersion.Patterns.FactoryAssoc
{
    public abstract class AbstractAssocFactory 
    {
        public IAssoc CreateAssociationFromAssocFactory(IAssoc aAssociation)
        {
            IAssoc association = CreateAssociation(aAssociation);
            return association;
             
        }
        public abstract IAssoc CreateAssociation(IAssoc aAssociation);
    }
}

