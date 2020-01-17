using System;
using PetCareFinalVersion.Models;

namespace PetCareFinalVersion.Data
{
    public interface IPost
    {
<<<<<<< HEAD
        
=======
         int Id { get; set; }


         int Association_id { get; set; }
         Association Association { get; set; }

   
         string Title { get; set; }
    
         string Description { get; set; }
    
         DateTime Date { get; set; }

       
         string Type { get; set; }
>>>>>>> master
    }
}
