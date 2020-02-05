using PetCareFinalVersion.Models;
using PetCareFinalVersion.Patterns.Observer;

namespace PetCareFinalVersion.Patterns.StateMachine
{
    public class Adopted : AbstractStatus, ISubject
    {
        public override string LostTo()
        {
            // Observer
            notify((Animal)_context);
            return this._context.TransistionTo(new Lost());
        }
        
        public override string ToAdoption()
        {
            return this._context.TransistionTo(new Adoption());
        }

        public override string GetTypeOf()
        {
            return "Adotado";
        }

        public void notify(Animal aAnimal)
        {
            throw new System.NotImplementedException();
        }
    }
}
