using System;
using PetCareFinalVersion.Models;

namespace PetCareFinalVersion.Patterns.Observer
{
    public interface ISubject
    {
        void notify(Animal aAnimal);
    }
}
