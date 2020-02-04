using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareFinalVersion.Data
{
    public interface IUser
    {
         int Id { get; set; }
        
         string Name { get; set; }
   
         string Email { get; set; }

         string Password { get; set; }

         bool Admin { get; set; }
    }
}
