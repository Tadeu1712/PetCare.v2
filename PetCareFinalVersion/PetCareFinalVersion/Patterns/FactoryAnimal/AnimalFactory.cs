using System;
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

        public override AbstractAnimal CreateAnimal(IFormCollection data, int assoc_id)
        {
           var animal = new Animal()
           {
                Name = data["name"][0],
                Description = data["description"][0],
                Age = DateTime.Parse(data["age"][0]),
                Type = data["type"][0],
                Weight = float.Parse(data["weight"][0]),
                Size= data["size"][0],
                Breed = data["breed"][0],
                Association_id = assoc_id,
            };
           animal.Status= animal.TransistionTo(new Adoption());
            
           return animal;
        }
    }
}




