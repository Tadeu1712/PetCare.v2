namespace PetCareFinalVersion.Patterns.StateMachine
{
    public class Adopted : AbstractStatus
    {
        public override string LostTo()
        {
            // Observer
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
    }
}
