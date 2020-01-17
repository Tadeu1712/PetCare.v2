using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareFinalVersion.Data;
using Animal = PetCareFinalVersion.Models.Animal;

namespace PetCareFinalVersion.Patterns
{
    public abstract class AbstractAnimalFactory
    {
        public AbstractAnimal CreateAnimalFromAnimalFactory(string aName, string aAge, int aWeight, string aType, string aBreed, string aDescription, int aAssociationId)
        {
            AbstractAnimal animal = CreateAnimal(aName,  aAge,  aWeight,  aType,  aBreed,  aDescription, aAssociationId );
            return animal;

        }
        public abstract AbstractAnimal CreateAnimal(string aName, string aAge, int aWeight, string aType, string aBreed, string aDescription, int aAssociationId);
    }
}

