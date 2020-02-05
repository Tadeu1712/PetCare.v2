using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Models
{

    public class Association : IAssoc
    {

       
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(32)]
        public string Iban { get; set; }
        [Required]
        [MaxLength(255)]
        public string Adress { get; set; }
        [Required]
        [MaxLength(9)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        public string FoundationDate { get; set; }


        [ForeignKey("User")]
        public int User_id { get; set; }
        public User User { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Animal> Animals { get; set; }

        public string Image { get; set; }
    }


}

