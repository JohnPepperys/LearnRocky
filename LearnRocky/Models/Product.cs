using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnRocky.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(1, 100000, ErrorMessage = "Price Not Defined")]
        public double Price { get; set; }
        public string? imageUrl { get; set; }
        [Display(Name = "Category Type")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? categories { get; set; }
    }
}
