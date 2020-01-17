using PetCareFinalVersion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareFinalVersion.Data
{
    public static class Auth
    {

        public static User Login(string aEmail, string aPass, AppDbContext _context)
        {

            var user = _context.Users.Where(u => u.Email == aEmail).Single();


            if (BCrypt.Net.BCrypt.EnhancedVerify(aPass, user.Password))
            {
                return user;
            }
            else
            {
                return null;
            }

        }
    }
}
