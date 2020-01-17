using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Models
{
    public class Animal : AbstractAnimal
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [MaxLength(50)]
        public string Breed { get; set; }

        [Required]
        public string Age { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [ForeignKey("Association")]
        public int Association_id { get; set; }
        public  Association Association { get; set; }


        public ICollection<AnimalImage> Images { get; set; }


    }
}


