using System.Linq;
using System.Web.Mvc;
using DataModel;
using EShop.Models;

namespace EShop.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly DbModel _context = new DbModel();

        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = GetCart();

            var model = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotalPrice()
            };

            return View(model);
        }

        // POST: /ShoppingCart/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ShoppingCartViewModel model)
        {
            var cart = GetCart();

            if (ModelState.IsValid)
            {
                cart.Update(model.CartItems);
            }

            model.CartItems = cart.GetCartItems();
            model.CartTotal = cart.GetTotalPrice();

            return View(model);
        }

        // GET: /ShoppingCart/AddToCart/5/10
        public ActionResult AddToCart(int id, int quantity)
        {
            var product = _context.Products
                .Single(p => p.Id == id);

            var cart = GetCart();
            cart.AddToCart(product, quantity);

            // Redirect tp shopping cart
            return RedirectToAction("Index");
        }

        // GET: /ShoppingCart/RemoveFromCart/5
        public ActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();
            cart.RemoveFromCart(id);

            // Redirect tp shopping cart
            return RedirectToAction("Index");
        }

        private ShoppingCart GetCart()
        {
            return ShoppingCart.GetCart(User.Identity.Name);
        }
    }
}
