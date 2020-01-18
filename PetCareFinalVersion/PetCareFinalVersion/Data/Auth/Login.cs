using PetCareFinalVersion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareFinalVersion.Data
{
    public class Login
    {
        public string email { get; set; }
        public string password { get; set; }

        public Login(string aEmail, string aPassword)
        {
            email = aEmail;
            password = aPassword;
        }

    }
}
