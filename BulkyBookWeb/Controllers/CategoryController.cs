using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            //var cate = _db.Categories.ToList();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(category.Name==category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("customError", "Name and display error values cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _db.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        //GET
        public IActionResult Edit(int?id)
        {
            if (id == null || id == 0) return NotFound();

            var category = _db.Categories.Find(id);

            if(category == null) return NotFound(); 


            return View(category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("customError", "Name and display error values cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _db.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

       


    }
}
