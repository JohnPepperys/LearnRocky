using LearnRocky.Data;
using LearnRocky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using LearnRocky.Models.ViewModels;
using LearnRocky.Utils;

namespace LearnRocky.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplictionDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplictionDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM {
                prod = _db.Products.Include(u => u.categories),
                categories = _db.Categories,
            };   
            return View(homeVM);
        }

        public IActionResult AddToCart(int? id)
        {
            if  (id == null || id < 1)
            {
                return RedirectToAction("Index");
            }
            uint productInCart = 0;
            if (HttpContext.Session.GetString(WebConst.SessionCart) != null)
            {
                var shopCart = HttpContext.Session.Read<List<ShoppingCart>>(WebConst.SessionCart);
                var shop = shopCart.Where(u => u.ProductId == id).FirstOrDefault();
                if (shop != null)
                {
                    productInCart += shop.quantity;
                }
            }
            AddCartModel prod = new AddCartModel
            {
                product = _db.Products.FirstOrDefault(u => u.id == id),
                quantity = 0,
                inCart = productInCart
            };
            return View(prod);
        }

        [HttpPost]
        public IActionResult AddToCart(AddCartModel? prod)
        {
            if (prod != null)
            {
                var _session = HttpContext.Session;
                List <ShoppingCart> shopList = new List<ShoppingCart>();
                // check session - get data from session
                if (_session.GetString(WebConst.SessionCart) != null)
                {
                    shopList = _session.Read<List<ShoppingCart>>(WebConst.SessionCart);
                }
                shopList.Add(new ShoppingCart
                {
                    ProductId = prod.product.id,
                    quantity = prod.quantity
                });
                _session.Set<List<ShoppingCart>>(WebConst.SessionCart, shopList);
            }
            return RedirectToAction("Index");
        }


        //GET from Cart
        public IActionResult Cart()
        {
            if (HttpContext.Session.GetString(WebConst.SessionCart) != null)
            {
                List<AllCart> allCart = new List<AllCart>();

                var shopCart = HttpContext.Session.Read<List<ShoppingCart>>(WebConst.SessionCart);
                foreach (var cart in shopCart)
                {
                    allCart.Add(new AllCart
                    {
                        prod = _db.Products.FirstOrDefault(u => u.id == cart.ProductId),
                        quantity = cart.quantity,
                        price = cart.quantity * _db.Products.FirstOrDefault(u => u.id == cart.ProductId).Price
                    });
                }
                return View(allCart);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult DeleteCartItem(int? id)
        {
            if (id != null && id > 0)
            {
                if (HttpContext.Session.GetString(WebConst.SessionCart) != null)
                {
                    var allCart = HttpContext.Session.Read<List<ShoppingCart>>(WebConst.SessionCart);
                    var temp = allCart.Where(u => u.ProductId == id).FirstOrDefault();
                    if (temp != null)
                    {
                        allCart.Remove(temp);
                        HttpContext.Session.Set<List<ShoppingCart>>(WebConst.SessionCart, allCart);
                    }

                    if (allCart.Count == 0) { 
                        RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Cart");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}