using Microsoft.AspNetCore.Http;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Patterns
{
    public abstract class AbstractAnimalFactory
    {
        public AbstractAnimal CreateAnimalFromAnimalFactory(IFormCollection data)
        {
            AbstractAnimal animal = CreateAnimal(data);
            return animal;

        }
        public abstract AbstractAnimal CreateAnimal(IFormCollection data);
    }
}

