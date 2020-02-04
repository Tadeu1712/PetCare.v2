using Microsoft.AspNetCore.Http;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Patterns
{
    public abstract class AbstractAnimalFactory
    {
        public AbstractAnimal CreateAnimalFromAnimalFactory(IFormCollection data, int assoc_id)
        {
            AbstractAnimal animal = CreateAnimal(data, assoc_id);
            return animal;

        }
        public abstract AbstractAnimal CreateAnimal(IFormCollection data, int assoc_id);
    }
}

