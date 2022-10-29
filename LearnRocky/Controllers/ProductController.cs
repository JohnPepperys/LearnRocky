using LearnRocky.Data;
using Microsoft.AspNetCore.Mvc;
using LearnRocky.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using LearnRocky.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace LearnRocky.Controllers
{
    [Authorize(Roles = WebConst.AdminOrManagerRole)]
    public class ProductController : Controller
    {
        private readonly ApplictionDbContext _db;
        private readonly IWebHostEnvironment _webEnv;
        public ProductController(ApplictionDbContext db, IWebHostEnvironment iwhe)
        {
            _db = db;
            _webEnv = iwhe;
        }

        public IActionResult IndexProduct()
        {
            IEnumerable<Product> objList = _db.Products.Include(u => u.categories);
            return View(objList);
        }
        
        
        // ------------------------------------- Create side ---------------------
        // GET - create
        public IActionResult CreateProduct(int id)
        {
            Product temp_prod = null;
            string temp_image = null;
            if (id > 0)
            {
                temp_prod = _db.Products.Find(id);
                temp_image = temp_prod.imageUrl;
            }
            ProductVM prod = new ProductVM
            {
                selectCategory = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CategoryId.ToString()
                }),
                product = temp_prod,
                temp_image_url = temp_image
            };
            // ---------------------- 
            return View(prod);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CreateProduct(ProductVM prod)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(files[0].FileName);
                string fullfilepath = Path.Combine(_webEnv.WebRootPath, WebConst.imagePath, filename);
                using (var sf = new FileStream(fullfilepath, FileMode.Create))
                {
                    files[0].CopyToAsync(sf);
                }
                prod.product.imageUrl = filename;
            }
            else
            {
                if (prod.product.id > 0) {
                    prod.product.imageUrl = prod.temp_image_url;
                } 
            }
            _db.Products.Update(prod.product);
            _db.SaveChanges();
            return RedirectToAction("IndexProduct");
        }

        // -------------------------- Delete - site ---------------------
        // GET - Delete
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product prod = _db.Products.Include(u => u.categories).FirstOrDefault(u => u.id == id);
            if (prod == null) { return NotFound(); }
            return View(prod);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DeleteProduct(Product prod)
        {
            try
            {
                string temppath = Path.Combine(_webEnv.WebRootPath, WebConst.imagePath, prod.imageUrl);
                if (System.IO.File.Exists(temppath)) {
                    System.IO.File.Delete(temppath);
                }
                _db.Products.Remove(prod);
                _db.SaveChanges();
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("ErrorDeleteProduct");
            }
            return RedirectToAction("IndexProduct");
        }

        public IActionResult ErrorDeleteProduct()
        {
            return View();
        }
    }
}
