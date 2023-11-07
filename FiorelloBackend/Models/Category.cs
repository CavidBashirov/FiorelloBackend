using System.ComponentModel.DataAnnotations;

namespace FiorelloBackend.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Can't be empty")]
        [MaxLength(10,ErrorMessage = "Max length must be 10 symbols")]
        
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
