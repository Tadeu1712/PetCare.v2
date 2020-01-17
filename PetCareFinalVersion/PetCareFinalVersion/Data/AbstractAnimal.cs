using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareFinalVersion.Models;
using PetCareFinalVersion.Patterns;
using PetCareFinalVersion.Patterns.StateMachine;

namespace PetCareFinalVersion.Data
{

    public abstract class AbstractAnimal
    {

        private AbstractStatus _state = null;

        public string Name { get; set; }

        public string Type { get; set; }

        public string Breed { get; set; }

        public string Age { get; set; }

        public int Weight { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public Association Association { get; set; }

        public int Association_id { get; set; }

        public string Image { get; set; }




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



        // Ricardo Crias-te esta função e nunca a usaste é realmente preciso?
        public void SetParams(AbstractStatus state, string aName, string aAge, int aWeight, string aType, string aBreed, string aDescription, Association aAssociation)
        {
            this.Name = aName;
            this.Age = aAge;
            this.Breed = aBreed;
            this.Weight = aWeight;
            this.Type = aType;
            this.Description = aDescription;
            this.TransistionTo(state);
            this.Association = aAssociation;
            this.Association_id = aAssociation.Id;

        }

    }
  
}
