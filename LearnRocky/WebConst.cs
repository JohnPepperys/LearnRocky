namespace LearnRocky
{
    public static class WebConst
    {
        public static string imagePath = Path.Combine("image", "products");
        public static string SessionCart = "ShoppingCartSession";
        public const string AdminRole = "Administrator";
        public const string CustomerRole = "Client";
        public const string ManagerRole = "Manager";
        public const string AdminOrManagerRole = AdminRole + "," + ManagerRole;
    }
}
