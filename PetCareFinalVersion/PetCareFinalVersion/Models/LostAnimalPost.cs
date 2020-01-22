using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Models
{
    public class LostAnimalPost : IPost
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

     
        public string Image { get; set; }


    }
}
