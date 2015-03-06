using System.Linq;
using System.Web.Mvc;
using DataModel;
using EShop.Models;

namespace EShop.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly DbModel _context = new DbModel();

        // GET: /Basket/
        public ActionResult Index()
        {
            var basket = GetBasket();

            var model = new BasketViewModel
            {
                BasketItems = basket.GetBasketItems(),
                BasketTotal = basket.GetTotalPrice()
            };

            return View(model);
        }

        // POST: /Basket/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(BasketViewModel model)
        {
            var basket = GetBasket();

            if (ModelState.IsValid)
            {
                basket.Update(model.BasketItems);
                return RedirectToAction("Index");
            }

            model.BasketItems = basket.GetBasketItems();
            model.BasketTotal = basket.GetTotalPrice();

            return View(model);
        }

        // GET: /Basket/AddToBasket/5/10
        public ActionResult AddToBasket(int id, int quantity)
        {
            var product = _context.Products
                .Single(p => p.Id == id);

            var basket = GetBasket();
            basket.AddToBasket(product, quantity);

            // Redirect tp basket
            return RedirectToAction("Index");
        }

        // GET: /Basket/RemoveFromBasket/5
        public ActionResult RemoveFromBasket(int id)
        {
            var basket = GetBasket();
            basket.RemoveFromBasket(id);

            // Redirect tp basket
            return RedirectToAction("Index");
        }

        private Basket GetBasket()
        {
            return Basket.GetBasket(User.Identity.Name);
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
