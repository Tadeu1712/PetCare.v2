using Microsoft.AspNetCore.Http;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Patterns.FactoryUser
{
    public abstract class AbstractUserFactory 
    {
        public IUser CreateUserFromFactory(IFormCollection data)
        {
            IUser user = CreateUser(data);
            return user;

        }
        public abstract IUser CreateUser(IFormCollection data);
    }
}

