using System.ComponentModel.DataAnnotations;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Models
{
       public class User :IUser
        {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        
        public string Password { get; set; }
        [Required]
        public bool Admin { get; set; }
        
    }


}
