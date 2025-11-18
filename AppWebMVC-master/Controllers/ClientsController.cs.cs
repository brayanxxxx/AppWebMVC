using Microsoft.AspNetCore.Mvc;
using WebAppProductsandCategories.Data;
using WebAppProductsandCategories.Models;

namespace WebAppProductsandCategories.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ClientData _clientData;

        public ClientsController(IConfiguration configuration)
        {
            _clientData = new ClientData(configuration);
        }

        public IActionResult Index()
        {
            var clients = _clientData.GetClients();
            return View(clients);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _clientData.CreateClient(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        public IActionResult Edit(int id)
        {
            var client = _clientData.GetClientById(id);
            return View(client);
        }

        [HttpPost]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                _clientData.UpdateClient(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        public IActionResult Delete(int id)
        {
            var client = _clientData.GetClientById(id);
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _clientData.DeleteClient(id);
            return RedirectToAction("Index");
        }
    }
}


