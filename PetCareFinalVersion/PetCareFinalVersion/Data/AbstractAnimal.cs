using System;
using PetCareFinalVersion.Models;
using PetCareFinalVersion.Patterns.StateMachine;

namespace PetCareFinalVersion.Data
{
    public abstract class AbstractAnimal
    {

        private AbstractStatus _state = null;

        public abstract int Id { get; set; }
        public abstract string Name { get; set; }
        public abstract string Type { get; set; }
        public abstract string Breed { get; set; }
        public abstract string Age { get; set; }
        public abstract float Weight { get; set; }
        public abstract string Status { get; set; }
        public abstract string Size { get; set; }
        public abstract string Description { get; set; }
        public abstract Association Association { get; set; }
        public abstract int Association_id { get; set; }
        public abstract string Image { get; set; }


        //FUNÇÃO QUE PERMITE TRANSITAR DE ESTADO PARA OUTRO ESTADO
        public string TransistionTo(AbstractStatus state)
        {
            this._state = state;
            this._state.SetContext(this);
            this.Status = state.GetTypeOf();
            return state.GetTypeOf();
        }

        //AÇÃO REQUEST1
        public string StartLosted()
        {
            return this._state.LostTo();
        }

        //AÇÃO REQUEST2
        public string StartToAdoption()
        {
           return this._state.AdoptedTo();
        }

        public string  StartAdopted()
        {
           return this._state.ToAdoption();
        }

    }
  
}
