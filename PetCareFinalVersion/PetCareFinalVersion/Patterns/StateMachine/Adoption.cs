namespace PetCareFinalVersion.Patterns.StateMachine
{
    public class Adoption : AbstractStatus
    {
        public override string LostTo()
        {
            //Observer
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
