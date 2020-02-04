using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Models
{
    public class Post : IPost
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Association")]
        public int Association_id { get; set; }
        public Association Association { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

        public string Image { get; set; }


    }
}
