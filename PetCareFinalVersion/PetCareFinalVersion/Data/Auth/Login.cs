using PetCareFinalVersion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareFinalVersion.Data
{
    public class Login
    {
        public User mUser { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public Login(string aEmail, string aPass)
        {
            email = aEmail;
            pass = aPass;
        }


    }
}
