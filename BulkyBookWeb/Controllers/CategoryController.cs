﻿using BulkyBookWeb.Data;
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
    }
}
