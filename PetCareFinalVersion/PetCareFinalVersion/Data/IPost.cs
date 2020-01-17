using System;
using PetCareFinalVersion.Models;

namespace PetCareFinalVersion.Data
{
    public interface IPost
    {

        int Id { get; set; }
   
        string Title { get; set; }
    
        string Description { get; set; }

        string Image { get; set; }   

    }
}
