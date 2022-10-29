using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnRocky.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 64000, ErrorMessage = "We need normal number between 1 and 64000")]
        [DisplayName("Display Order")]
        public int CategoryLevel { get; set; }
    }
}
