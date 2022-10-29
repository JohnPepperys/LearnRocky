using System.ComponentModel.DataAnnotations;

namespace LearnRocky.Models.ViewModels
{
    public class AddCartModel
    {
        public Product? product { get; set; }
        [Required]
        [Range(1, 1200, ErrorMessage = "We need normal quantity between 1 and 1200")]
        public uint quantity { get; set; } 
        public uint inCart { get; set; }
    }
}
