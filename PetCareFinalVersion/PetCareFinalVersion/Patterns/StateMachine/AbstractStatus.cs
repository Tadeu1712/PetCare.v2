using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Patterns.StateMachine
{
    public abstract class AbstractStatus : IState
    {
        protected AbstractAnimal _context;
        public void SetContext(AbstractAnimal context)
        {
            this._context = context;
        }

        //AÇÕES POSSIVEIS NA MAQUINA DE ESTADOS
        public virtual string LostTo(){ return "Perdido"; }
        public virtual string AdoptedTo(){ return "Adotado"; }
        public virtual string ToAdoption() { return "Adoção"; }

        public abstract string GetTypeOf();
    }

    
}
