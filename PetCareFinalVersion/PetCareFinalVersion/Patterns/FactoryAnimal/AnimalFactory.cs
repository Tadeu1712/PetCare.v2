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

        public override AbstractAnimal CreateAnimal(string aName, string aAge, int aWeight, string aType, string aBreed,
            string aDescription, int aAssociationId)
        {

            var Animal = new Animal()
            {
                Name = aName,
                Description = aDescription,
                Age = aAge,
                Type = aType,
                Weight = aWeight,
                Breed = aBreed,
                Association_id = aAssociationId,
            };
            Animal.Status= Animal.TransistionTo(new Adoption());
            
            return Animal;
        }
    }
}




