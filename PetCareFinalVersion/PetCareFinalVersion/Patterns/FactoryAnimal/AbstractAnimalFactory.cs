using Microsoft.AspNetCore.Http;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Models;

namespace PetCareFinalVersion.Patterns
{
    public abstract class AbstractAnimalFactory
    {
        public AbstractAnimal CreateAnimalFromAnimalFactory(Animal aAnimal)
        {
            AbstractAnimal animal = CreateAnimal(aAnimal);
            return animal;

        }
        public abstract AbstractAnimal CreateAnimal(Animal aAnimal);
    }
}

