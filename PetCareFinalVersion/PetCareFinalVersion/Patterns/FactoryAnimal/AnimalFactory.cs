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
                Name = data["aName"][0],
                Description = data["aDescription"][0],
                Age = data["aAge"][0],
                Type = data["aType"][0],
                Weight = float.Parse(data["aWeight"][0]),
                Size= data["aSize"][0],
                Breed = data["aBreed"][0],
                Association_id = assoc_id,
            };
           animal.Status= animal.TransistionTo(new Adoption());
            
           return animal;
        }
    }
}




