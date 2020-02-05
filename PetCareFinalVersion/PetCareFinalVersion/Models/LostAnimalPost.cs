using System;
using System.ComponentModel.DataAnnotations;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Models
{
    public class LostAnimalPost : IPost
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Location { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

     
        public string Image { get; set; }


    }
}
