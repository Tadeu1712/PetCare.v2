using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Models;

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

