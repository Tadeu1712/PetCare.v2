namespace PetCareFinalVersion.Patterns.StateMachine
{
    public class Lost :AbstractStatus
    {

        public override string AdoptedTo()
        {
           return this._context.TransistionTo(new Adopted());
        }

        public override string ToAdoption() 
        {
            return this._context.TransistionTo(new Adoption());
        }
        public override string GetTypeOf()
        {
            return "Perdido";
        }
    }
}
