using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Models
{
    public class Event : IPost
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Association")]
        public int Association_id { get; set; }
        public Association Association { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        [MaxLength(250)]
        public string Location{ get; set; }

        [Required]
        public DateTime DateInit { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type {get;set;}

        [Required]
        public decimal Price { get; set; }

        public  string Image{ get; set; }


    }
}
