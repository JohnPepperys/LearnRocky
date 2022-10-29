using LearnRocky.Data;
using LearnRocky.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnRocky.Controllers
{
    [Authorize(Roles = WebConst.AdminOrManagerRole)]
    public class CategoryController : Controller
    {
        private readonly ApplictionDbContext _db;
        public CategoryController(ApplictionDbContext db)  { 
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Categories;
            return View(objList);
        }

        // GET - create
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(cat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            } else { 
                return View(cat); 
            }
        }

        // GET - Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) {
                return NotFound();
            }
            Category cat = _db.Categories.Find(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Category cat)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(cat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(cat);
            }
        }

        // GET - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category cat = _db.Categories.Find(id);
            if (cat == null) { return NotFound(); }
            return View(cat);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DeletePost(Category cat)
        {
            _db.Categories.Remove(cat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
