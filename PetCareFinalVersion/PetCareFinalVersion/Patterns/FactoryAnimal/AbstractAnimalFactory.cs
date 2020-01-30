using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PetCareFinalVersion.Data;
using Animal = PetCareFinalVersion.Models.Animal;

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

