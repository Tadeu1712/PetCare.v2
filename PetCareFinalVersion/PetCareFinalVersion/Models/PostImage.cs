using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareFinalVersion.Models
{
    public class PostImage
    {
       
        [Key]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }

        [ForeignKey("Post")]
        public int Post_id { get; set; }
        public Post Post { get; set; }

        
       
    }
}
