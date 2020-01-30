using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public override AbstractAnimal CreateAnimal(IFormCollection data)
        {
           var Animal = new Animal()
            {
                Name = data["aName"][0],
                Description = data["aDescription"][0],
                Age = DateTime.Parse(data["aAge"][0]),
                Type = data["aType"][0],
                Weight = float.Parse(data["aWeight"][0]),
                Size= data["aSize"][0],
                Breed = data["aBreed"],
                Association_id = int.Parse(data["aAssociation_id"][0]),
            };
            Animal.Status= Animal.TransistionTo(new Adoption());
            
            return Animal;
        }
    }
}




