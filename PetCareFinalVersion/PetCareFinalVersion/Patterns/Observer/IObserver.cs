using System;
namespace PetCareFinalVersion.Patterns.Observer
{
    public interface IObserver
    {
        void update(string title, string description, string location, string contact, string image);
    }
}
