using Microsoft.AspNetCore.Mvc;
using WebAppProductsandCategories.Data;
using WebAppProductsandCategories.Models;


namespace WebAppProductsandCategories.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductData _dataAccess;
        private readonly CategoryData _dataAccessCat;

        public ProductController(ProductData dataAccess, CategoryData dataAccessCategory)
        {
            _dataAccess = dataAccess;
            _dataAccessCat = dataAccessCategory;
        }
        public IActionResult Index()
        {
            var products = _dataAccess.GetProducts();
            ViewBag.Categories = GetCategories();
            return View(products);
        }


        // Create - Show Form
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategories(); // For dropdown list
            return View();
        }

        // Create - Process Form
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _dataAccess.CreateProduct(product);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = GetCategories(); // For dropdown list
            return View(product);
        }

        // Edit - Show Form
        public IActionResult Edit(int id)
        {
            var product = _dataAccess.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = GetCategories(); // For dropdown list
            return View(product);
        }

        // Edit - Process Form
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _dataAccess.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = GetCategories(); // For dropdown list
            return View(product);
        }

        // Delete - Show Form
        public IActionResult Delete(int id)
        {
            var product = _dataAccess.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Delete - Process Form
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int ProductId)
        {
            _dataAccess.DeleteProduct(ProductId);
            return RedirectToAction("Index");
        }

        private List<Category> GetCategories()
        {
            return _dataAccessCat.GetCategories();
        }

    }

}
