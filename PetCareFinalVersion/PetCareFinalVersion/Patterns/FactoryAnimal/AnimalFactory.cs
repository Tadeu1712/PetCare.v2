using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Models;
using PetCareFinalVersion.Patterns.StateMachine;


namespace PetCareFinalVersion.Patterns
{
    public sealed class AnimalFactory: AbstractAnimalFactory
    {
        private static readonly AnimalFactory mInstance = new AnimalFactory();

        private AnimalFactory() { }
        static AnimalFactory() { }

        public static AnimalFactory Instance => mInstance;

        public override AbstractAnimal CreateAnimal(AbstractAnimal aAnimal)
        {
           var Animal = new Animal()
            {
                Name = aAnimal.Name,
                Description = aAnimal.Description,
                Age = aAnimal.Age,
                Type = aAnimal.Type,
                Weight = aAnimal.Weight,
                Size= aAnimal.Size,
                Breed = aAnimal.Breed,
                Association_id = aAnimal.Association_id,
            };
            Animal.Status= Animal.TransistionTo(new Adoption());
            
            return Animal;
        }
    }
}




