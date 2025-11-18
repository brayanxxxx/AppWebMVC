using Microsoft.AspNetCore.Mvc;
using WebAppProductsandCategories.Data;
using WebAppProductsandCategories.Models;

namespace WebAppProductsandCategories.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeData _employeeData;

        public EmployeeController(IConfiguration configuration)
        {
            _employeeData = new EmployeeData(configuration);
        }

        public IActionResult Index()
        {
            var employees = _employeeData.GetEmployees();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeData.CreateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _employeeData.GetEmployeeById(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeData.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var employee = _employeeData.GetEmployeeById(id);
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeeData.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var employee = _employeeData.GetEmployeeById(id);
            return View(employee);
        }
    }
}
