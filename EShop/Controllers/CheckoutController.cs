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
            // Customer should not be here if there is nothing in the basket
            if (basket.IsEmpty())
            {
                return RedirectToAction("Index", "Basket");
            }

            var model = new CheckoutViewModel
            {
                Customer = GetCustomer(),
                Warehouses = WarehouseItemsSelectList(),
                OrderTotal = basket.GetTotalPrice(),
                OrderItems = basket.GetBasketItems(),
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
                var orderId = basket.CreateOrder(model.WarehouseId);
                TempData["successMessage"] = "New order was successfully created with ID #" + orderId + ".";
                return RedirectToAction("Details", "Orders", new {id = orderId});
            }

            model.Customer = GetCustomer();
            model.Warehouses = WarehouseItemsSelectList();
            model.OrderTotal = basket.GetTotalPrice();
            model.OrderItems = basket.GetBasketItems();

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
