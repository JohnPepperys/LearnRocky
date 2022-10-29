using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearnRocky.Models.ViewModels
{
    public class ProductVM
    {
        public Product? product { get; set; }
        public IEnumerable<SelectListItem> selectCategory { get; set; }
        public string? temp_image_url { get; set; }
    }
}
