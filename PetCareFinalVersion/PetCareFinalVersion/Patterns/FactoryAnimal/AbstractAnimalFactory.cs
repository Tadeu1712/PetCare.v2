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
        public AbstractAnimal CreateAnimalFromAnimalFactory(AbstractAnimal aAnimal)
        {
            AbstractAnimal animal = CreateAnimal(aAnimal);
            return animal;

        }
        public abstract AbstractAnimal CreateAnimal(AbstractAnimal aAnimal);
    }
}

