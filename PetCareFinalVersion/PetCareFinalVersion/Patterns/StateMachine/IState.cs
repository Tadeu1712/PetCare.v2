using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareFinalVersion.Patterns.StateMachine
{
    public interface IState
    {
        string LostTo();
        string AdoptedTo();
        string ToAdoption();
    }
}
