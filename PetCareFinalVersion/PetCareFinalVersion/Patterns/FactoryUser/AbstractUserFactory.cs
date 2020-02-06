using Microsoft.AspNetCore.Http;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Models;

namespace PetCareFinalVersion.Patterns.FactoryUser
{
    public abstract class AbstractUserFactory 
    {
        public IUser CreateUserFromFactory(IUser aUser)
        {
            IUser user = CreateUser(aUser);
            return user;

        }
        public abstract IUser CreateUser(IUser aUser);
    }
}

