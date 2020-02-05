using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Patterns.StateMachine;

namespace PetCareFinalVersion.Models
{
    public class Animal : AbstractAnimal
    {

        [Key]
        public override int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public override string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public override string Type { get; set; }

        [Required]
        [MaxLength(50)]
        public override string Breed { get; set; }

        [Required]
        public override DateTime Age { get; set; }

        [Required]
        public override float Weight { get; set; }

        [Required]
        [MaxLength(50)]
        public override string Size { get; set; }

        [Required]
        [MaxLength(50)]
        public override string Status { get; set; }
        


        [Required]
        [MaxLength(250)]
        public override string  Description { get; set; }

        [ForeignKey("Association")]
        public override int Association_id { get; set; }
        public override Association Association { get; set; }


        public override string Image { get; set; }


    }
}


