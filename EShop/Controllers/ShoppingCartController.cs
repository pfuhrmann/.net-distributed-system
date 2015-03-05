using System.Linq;
using System.Web.Mvc;
using DataModel;
using EShop.Models;

namespace EShop.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        readonly DbModel _context = new DbModel();

        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(User.Identity.Name);

            var model = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(model);
        }

        // GET: /ShoppingCart/AddToCart/5/10
        public ActionResult AddToCart(int id, int quantity)
        {
            var product = _context.Products
                .Single(p => p.Id == id);

            var cart = ShoppingCart.GetCart(User.Identity.Name);
            cart.AddToCart(product, quantity);

            // View shopping cart
            return RedirectToAction("Index");
        }

        // GET: /ShoppingCart/RemoveFromCart/5
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(User.Identity.Name);
            cart.RemoveFromCart(id);

            // View shopping cart
            return RedirectToAction("Index");
        }
    }
}
