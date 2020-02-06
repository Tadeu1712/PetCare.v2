using Microsoft.AspNetCore.Http;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Models;

namespace PetCareFinalVersion.Patterns.FactoryUser
{
    public sealed class UserFactory : AbstractUserFactory
    {
        private static readonly UserFactory mInstance = new UserFactory();

        private UserFactory() { }
        static UserFactory() { }

        public static UserFactory Instance => mInstance;


        public override IUser CreateUser(IUser aUser)
        {
            var user = new User() 
            {
                Name = aUser.Name,
                Email = aUser.Email,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(aUser.Password),
                Admin = false,
            };

            return user;
        }
    }
}
