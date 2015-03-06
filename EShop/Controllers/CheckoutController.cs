using System.Linq;
using System.Web.Mvc;
using DataModel;
using EShop.Models;
using Microsoft.AspNet.Identity;

namespace EShop.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly DbModel _context = new DbModel();

        // GET: Checkout
        public ActionResult Index()
        {
            // Prepare required data
            var basket = Basket.GetBasket(User.Identity.Name);
            var model = new CheckoutViewModel
            {
                Customer = GetCustomer(),
                Warehouses = WarehouseItemsSelectList(),
                OrderTotal = basket.GetTotalPrice()
            };

            return View(model);
        }

        // POST: /Checkout/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckoutViewModel model)
        {
            var basket = Basket.GetBasket(User.Identity.Name);

            if (ModelState.IsValid)
            {
                basket.CreateOrder();
            }

            model.Customer = GetCustomer();
            model.Warehouses = WarehouseItemsSelectList();
            model.OrderTotal = basket.GetTotalPrice();

            return View(model);
        }

        private SelectList WarehouseItemsSelectList()
        {
            var warehouseItems = _context.Warehouses.Select(
                w => new {w.Id, w.Name}).ToList();
            return new SelectList(warehouseItems, "Id", "Name");
        }

        private Customer GetCustomer()
        {
            var userId = User.Identity.GetUserId();
            return _context.Users.First(c => c.Id == userId);
        }
    }
}
