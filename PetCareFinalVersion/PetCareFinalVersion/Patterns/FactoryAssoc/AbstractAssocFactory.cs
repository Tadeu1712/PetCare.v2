using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Patterns.FactoryAssoc
{
    public abstract class AbstractAssocFactory 
    {
        public IUser CreateAssociationFromAssocFactory(IUser aAssociation)
        {
            IUser association = CreateAssociation(aAssociation);
            return association;

        }
        public abstract IUser CreateAssociation(IUser aAssociation);
    }
}

