using System;
using PetCareFinalVersion.Models;

namespace PetCareFinalVersion.Patterns.Observer
{
    public interface IObserver
    {
        void update(Animal aAnimal);
    }
}
