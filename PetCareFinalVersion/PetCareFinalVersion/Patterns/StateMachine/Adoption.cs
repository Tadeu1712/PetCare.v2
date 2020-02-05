using System.Collections.Generic;
using PetCareFinalVersion.Models;
using PetCareFinalVersion.Patterns.Observer;

namespace PetCareFinalVersion.Patterns.StateMachine
{
    public class Adoption : AbstractStatus, ISubject
    {
        public override string LostTo()
        {
            //Observer
            notify((Animal)_context);
            return this._context.TransistionTo(new Lost());
        }

        public override string AdoptedTo()
        {
            return this._context.TransistionTo(new Adopted());
        }

      
        public override string GetTypeOf()
        {
            return "Adoção";
        }

        public void notify(Animal animal)
        {
            
        }
    }
}
