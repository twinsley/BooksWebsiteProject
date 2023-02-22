﻿using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.GetAll();
            return View(objCategoryList);
        }
        // GET
        public IActionResult Create()
        {
            return View();
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDb = _db.GetFirstOrDefault(u => u.ID == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.GetFirstOrDefault(u => u.ID == id);
           // var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        // Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
           // var obj = _db.Categories.Find(id);
            var obj = _db.GetFirstOrDefault(u => u.ID == id);
            if (obj == null)
            {
                return NotFound();
            }
           
            _db.Remove(obj);
            _db.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
            
           
        }

    }

}
