using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataModel;
using EShop.Models;
using Microsoft.AspNet.Identity;

namespace EShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DbModel db = new DbModel();

        // GET: Products
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var model = new AddToBasketViewModel
            {
                Name = product.Name,
                Price = product.Price,
                Weight = product.Weight,
                BoxItemsAmount = product.BoxItemsAmount,
                Stock = product.StockTotal
            };

            return View(model);
        }

        // POST: Products/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(AddToBasketViewModel model, int? id)
        {
            if (ModelState.IsValid)
            {
                
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            model.Name = product.Name;
            model.Price = product.Price;
            model.Weight = product.Weight;
            model.BoxItemsAmount = product.BoxItemsAmount;
            model.Stock = product.StockTotal;

            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}