
namespace PetCareFinalVersion.Patterns.TemplateMethod
{
    public class ConcreteOk : AbstractTemplate
    {
        protected override void SetSuccess()
        {
            Success = true;
        }

        protected override void SetMessage(string aMsg)
        {
            Msg = aMsg;
        }

    }
}
