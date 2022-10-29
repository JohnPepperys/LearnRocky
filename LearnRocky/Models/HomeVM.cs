namespace LearnRocky.Models
{
    public class HomeVM
    {
        public IEnumerable<Product> prod { get; set; }
        public IEnumerable<Category> categories { get; set; }
    }
}
