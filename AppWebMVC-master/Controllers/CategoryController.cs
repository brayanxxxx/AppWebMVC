using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppProductsandCategories.Data;
using WebAppProductsandCategories.Models;

namespace WebAppProductsandCategories.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryData _dataAccess;


        public CategoryController(CategoryData dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Index - Display Categories
        public IActionResult Index()
        {
            var categories = _dataAccess.GetCategories();
            return View(categories);
        }

        // Create - Show Form
        public IActionResult Create()
        {
            return View();
        }

        // Create - Process Form
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _dataAccess.CreateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // Edit - Show Form
        public IActionResult Edit(int id)
        {
            var category = _dataAccess.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // Edit - Process Form
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _dataAccess.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // Delete - Show Form
        public IActionResult Delete(int id)
        {
            var category = _dataAccess.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // Delete - Process Form
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int CategoryId)
        {
            _dataAccess.DeleteCategory(CategoryId);
            return RedirectToAction("Index");
        }
    }
}
