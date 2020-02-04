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


        public override IUser CreateUser(IFormCollection data)
        {
            var user = new User() 
            {
                Name = data["name"][0],
                Email = data["email"][0],
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(data["password"][0]),
                Admin = false,
            };

            return user;
        }
    }
}
