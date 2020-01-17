using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareFinalVersion.Patterns.StateMachine
{
    public class Adoption : AbstractStatus
    {
        public override string LostTo()
        {
            return this._context.TransistionTo(new Lost());
        }

        public override string AdoptedTo()
        {
            return this._context.TransistionTo(new Adopted());
        }

      
        public override string GetTypeOf()
        {
            return "Para adoção";
        }
    }
}
