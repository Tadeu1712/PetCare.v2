using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareFinalVersion.Models
{
    public class AnimalImage
    {


        [Key]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }

        [ForeignKey("Animal")]
        public int Animal_id { get; set; }
        public Animal Animal { get; set; }
    }
}
