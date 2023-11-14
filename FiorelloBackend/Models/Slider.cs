using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiorelloBackend.Models
{
    public class Slider : BaseEntity
    {
        public string Img { get; set; }
        public bool Status { get; set; } = true;
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
    }
}
