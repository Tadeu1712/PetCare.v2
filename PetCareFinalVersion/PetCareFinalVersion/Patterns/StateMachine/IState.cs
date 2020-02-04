namespace PetCareFinalVersion.Patterns.StateMachine
{
    public interface IState
    {
        string LostTo();
        string AdoptedTo();
        string ToAdoption();
    }
}
